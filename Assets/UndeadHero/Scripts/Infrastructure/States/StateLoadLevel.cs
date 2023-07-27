using UndeadHero.CameraLogic;
using UndeadHero.Infrastructure.Factory;
using UnityEngine;

namespace UndeadHero.Infrastructure.States {
  class StateLoadLevel : IStatePayloaded<string> {
    private const string PlayerSpawnPointTag = "PlayerSpawnPoint";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly LoadingScreen _loadingScreen;
    private readonly IGameFactory _gameFactory;

    public StateLoadLevel(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingScreen, IGameFactory gameFactory) {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
      _gameFactory = gameFactory;
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

      _stateMachine.Enter<StateGameLoop>();
    }

    private void SetCameraFollowTarget(GameObject target) {
      if (Camera.main.TryGetComponent<CameraMover>(out var cameraMover)) {
        cameraMover.SetTarget(target);
      }
    }
  }
}
