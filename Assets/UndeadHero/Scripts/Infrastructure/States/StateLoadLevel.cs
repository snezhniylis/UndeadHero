using UndeadHero.CameraLogic;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.EventFactory;
using UndeadHero.Infrastructure.Services.Factory;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.Infrastructure.Services.StaticDataManagement;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.StaticData.Levels;
using UndeadHero.UI.Hud;
using UndeadHero.UI.LoadingScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UndeadHero.Infrastructure.States {
  public class StateLoadLevel : IStatePayloaded<string> {
    private const string PlayerSpawnPointTag = "PlayerSpawnPoint";
    private const string EnemySpawnerTag = "EnemySpawner";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;
    private readonly IEventFactory _eventFactory;
    private readonly IViewManager _viewManager;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataProvider _staticDataProvider;

    public StateLoadLevel(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory, IViewManager viewManager, IEventFactory eventFactory, IPersistentProgressService progressService, IStaticDataProvider staticDataProvider) {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
      _gameFactory = gameFactory;
      _eventFactory = eventFactory;
      _viewManager = viewManager;
      _progressService = progressService;
      _staticDataProvider = staticDataProvider;
    }

    public void Enter(string sceneName) {
      _loadingScreen.Show();
      _progressService.CleanUp();
      _viewManager.CleanUp();
      _sceneLoader.Load(sceneName, OnSceneLoaded);
    }

    public void Exit() =>
      _loadingScreen.Hide();

    private void OnSceneLoaded() {
      InitializeLevelEntities();
      _progressService.RestoreProgress();
      _viewManager.SpawnUiRoot();
      _stateMachine.Enter<StateGameLoop>();
    }

    private void InitializeLevelEntities() {
      GameObject hero = InitializeHero();
      SetMainCameraTarget(hero);

      InitializeEnemySpawners(hero);

      PlayerHud hud = InitializeHud(hero);

      InitializeGameEvents(hud);
    }

    private void InitializeGameEvents(PlayerHud hud) {
      foreach (GameEvent gameEvent in _eventFactory.CreateGameEvents()) {
        if (gameEvent.AreEventConditionsMet()) {
          hud.AddEventButton(gameEvent);
        }
      }
    }

    private GameObject InitializeHero() {
      Transform spawnPoint = GameObject.FindWithTag(PlayerSpawnPointTag).transform;
      return _gameFactory.CreateHero(spawnPoint.position, spawnPoint.rotation);
    }

    private void InitializeEnemySpawners(GameObject hero) {
      string currentLevelName = SceneManager.GetActiveScene().name;
      LevelStaticData levelData = _staticDataProvider.GetLevelData(currentLevelName);
      foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners) {
        _gameFactory.CreateEnemySpawner(spawnerData.Position, spawnerData.SpawnerId, spawnerData.EnemyTypeId, hero);
      }
    }

    private PlayerHud InitializeHud(GameObject hero) =>
      _gameFactory.CreateHud(hero);

    private static void SetMainCameraTarget(GameObject target) {
      var followTargetBehavior = Camera.main.GetComponent<CameraFollowTarget>();
      followTargetBehavior.SetTarget(target);
    }
  }
}
