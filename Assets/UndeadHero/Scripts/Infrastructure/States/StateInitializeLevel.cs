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
using VContainer;
using VContainer.Unity;
using GameObject = UnityEngine.GameObject;

namespace UndeadHero.Infrastructure.States {
  public class StateInitializeLevel : IState {
    private const string PlayerSpawnPointTag = "PlayerSpawnPoint";
    private const string InjectableObjectTag = "Injectable";

    private readonly IObjectResolver _sceneDiContainer;

    private readonly GameStateMachine _stateMachine;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;
    private readonly IEventFactory _eventFactory;
    private readonly IViewManager _viewManager;
    private readonly IPersistentProgressService _progressService;
    private readonly IStaticDataProvider _staticDataProvider;

    public StateInitializeLevel(IObjectResolver sceneDiContainer, GameStateMachine stateMachine, LoadingScreen loadingScreen, IGameFactory gameFactory, IEventFactory eventFactory, IViewManager viewManager, IPersistentProgressService progressService, IStaticDataProvider staticDataProvider) {
      _sceneDiContainer = sceneDiContainer;

      _stateMachine = stateMachine;
      _loadingScreen = loadingScreen;
      _gameFactory = gameFactory;
      _eventFactory = eventFactory;
      _viewManager = viewManager;
      _progressService = progressService;
      _staticDataProvider = staticDataProvider;
    }

    public void Enter() {
      InitializeLevelEntities();
      _progressService.RestoreProgress();
      _viewManager.SpawnUiRoot();
      _loadingScreen.Hide();
      _stateMachine.Enter<StateGameLoop>();
    }

    public void Exit() { }

    private void InitializeLevelEntities() {
      GameObject hero = InitializeHero();
      SetMainCameraTarget(hero);

      InitializeEnemySpawners();

      PlayerHud hud = InitializeHud();

      InitializeGameEvents(hud);

      InjectDependenciesToSceneGameObjects();
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

    private void InitializeEnemySpawners() {
      string currentLevelName = SceneManager.GetActiveScene().name;
      LevelStaticData levelData = _staticDataProvider.GetLevelData(currentLevelName);
      foreach (EnemySpawnerData spawnerData in levelData.EnemySpawners) {
        _gameFactory.CreateEnemySpawner(spawnerData.Position, spawnerData.SpawnerId, spawnerData.EnemyTypeId);
      }
    }

    private PlayerHud InitializeHud() =>
      _gameFactory.CreateHud();

    private void InjectDependenciesToSceneGameObjects() {
      foreach (GameObject gameObject in GameObject.FindGameObjectsWithTag(InjectableObjectTag)) {
        _sceneDiContainer.InjectGameObject(gameObject);
      }
    }

    private static void SetMainCameraTarget(GameObject target) {
      var followTargetBehavior = Camera.main.GetComponent<CameraFollowTarget>();
      followTargetBehavior.SetTarget(target);
    }
  }
}
