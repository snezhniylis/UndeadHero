using UnityEngine;
using UndeadHero.CameraLogic;

namespace UndeadHero.Infrastructure {
  class StateLoadScene : IStatePayloaded<string> {
    private const string PlayerSpawnPointTag = "PlayerSpawnPoint";

    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;

    public StateLoadScene(GameStateMachine gameStateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen) {
      _gameStateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
      // TODO: temporarily solution until I add a service locator
      _gameFactory = new GameFactory(new AssetManagement.AssetProvider());
    }

    public void Enter(string sceneName) {
      _loadingScreen.Show();
      _sceneLoader.Load(sceneName, OnSceneLoaded);
    }

    public void Exit() =>
      _loadingScreen.Hide();

    private void OnSceneLoaded() {
      _gameFactory.CreateHud();

      GameObject doge = _gameFactory.CreateHero(GameObject.FindWithTag(PlayerSpawnPointTag));
      SetCameraFollowTarget(doge);

      _gameStateMachine.Enter<StateGameLoop>();
    }

    private void SetCameraFollowTarget(GameObject target) {
      if (Camera.main.TryGetComponent<CameraMover>(out var cameraMover)) {
        cameraMover.SetTarget(target);
      }
    }
  }
}