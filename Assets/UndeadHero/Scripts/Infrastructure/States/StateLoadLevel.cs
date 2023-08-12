using UndeadHero.CameraLogic;
using UndeadHero.Infrastructure.Services.Factory;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.Level.Spawning;
using UndeadHero.UI.LoadingScreen;
using UnityEngine;

namespace UndeadHero.Infrastructure.States {
  public class StateLoadLevel : IStatePayloaded<string> {
    private const string PlayerSpawnPointTag = "PlayerSpawnPoint";
    private const string EnemySpawnerTag = "EnemySpawner";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;
    private readonly IPersistentProgressService _progressService;

    public StateLoadLevel(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory, IPersistentProgressService progressService) {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
      _gameFactory = gameFactory;
      _progressService = progressService;
    }

    public void Enter(string sceneName) {
      _loadingScreen.Show();
      _progressService.CleanUp();
      _sceneLoader.Load(sceneName, OnSceneLoaded);
    }

    public void Exit() =>
      _loadingScreen.Hide();

    private void OnSceneLoaded() {
      InitializeLevelEntities();
      _progressService.RestoreProgress();
      _stateMachine.Enter<StateGameLoop>();
    }

    private void InitializeLevelEntities() {
      GameObject hero = InitializeHero();
      SetMainCameraTarget(hero);

      InitializeEnemySpawners(hero);

      InitializeHud(hero);
    }

    private GameObject InitializeHero() {
      Transform spawnPoint = GameObject.FindWithTag(PlayerSpawnPointTag).transform;
      return _gameFactory.CreateHero(spawnPoint.position, spawnPoint.rotation);
    }

    private void InitializeEnemySpawners(GameObject hero) {
      foreach (GameObject spawner in GameObject.FindGameObjectsWithTag(EnemySpawnerTag)) {
        spawner.GetComponent<EnemySpawner>().Initialize(_gameFactory, hero);
        _progressService.BindSceneObject(spawner);
      }
    }

    private void InitializeHud(GameObject hero) =>
      _gameFactory.CreateHud(hero);

    private static void SetMainCameraTarget(GameObject target) {
      var followTargetBehavior = Camera.main.GetComponent<CameraFollowTarget>();
      followTargetBehavior.SetTarget(target);
    }
  }
}
