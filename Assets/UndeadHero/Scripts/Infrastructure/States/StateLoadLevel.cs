using UndeadHero.CameraLogic;
using UndeadHero.Character.Hero;
using UndeadHero.Infrastructure.Services.Factory;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.UI.Hud;
using UndeadHero.UI.LoadingScreen;
using UnityEngine;

namespace UndeadHero.Infrastructure.States {
  public class StateLoadLevel : IStatePayloaded<string> {
    private const string PlayerSpawnPointTag = "PlayerSpawnPoint";

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
      _progressService.ClearSubscribers();
      _sceneLoader.Load(sceneName, OnSceneLoaded);
    }

    public void Exit() =>
      _loadingScreen.Hide();

    private void OnSceneLoaded() {
      InitializeLevelEntities();
      _progressService.LoadProgress();
      _stateMachine.Enter<StateGameLoop>();
    }

    private void InitializeLevelEntities() {
      GameObject hero = InitializeHero();
      SetMainCameraTarget(hero);

      InitializeHud(hero);
    }

    private GameObject InitializeHero() =>
      _gameFactory.CreateHero(GameObject.FindWithTag(PlayerSpawnPointTag));

    private void InitializeHud(GameObject hero) {
      var hud = _gameFactory.CreateHud().GetComponent<PlayerHud>();
      hud.Initialize(hero.GetComponent<HeroHealth>());
    }

    private static void SetMainCameraTarget(GameObject target) {
      var followTargetBehavior = Camera.main.GetComponent<CameraFollowTarget>();
      followTargetBehavior.SetTarget(target);
    }
  }
}
