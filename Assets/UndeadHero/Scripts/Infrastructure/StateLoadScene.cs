using UnityEngine;
using UndeadHero.CameraLogic;

namespace UndeadHero.Infrastructure {
  class StateLoadScene : IStatePayloaded<string> {
    private const string HudPrefabPath = "UI/HUD";
    private const string DogePrefabPath = "Characters/Playable/Doge/Doge";
    private const string PlayerSpawnPointTag = "PlayerSpawnPoint";

    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public StateLoadScene(GameStateMachine gameStateMachine, SceneLoader sceneLoader) {
      _gameStateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter(string sceneName) =>
      _sceneLoader.Load(sceneName, OnSceneLoaded);

    public void Exit() { }

    private void OnSceneLoaded() {
      InstantiatePrefab(HudPrefabPath);

      var playerSpawnPoint = GameObject.FindWithTag(PlayerSpawnPointTag).transform;
      GameObject doge = InstantiatePrefab(DogePrefabPath, playerSpawnPoint.position, playerSpawnPoint.rotation);
      SetCameraFollowTarget(doge);

      _gameStateMachine.Enter<StateGameLoop>();
    }

    private static GameObject InstantiatePrefab(string path) =>
      InstantiatePrefab(path, Vector3.zero, Quaternion.identity);

    private static GameObject InstantiatePrefab(string path, Vector3 position, Quaternion rotation) {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, position, rotation);
    }

    private void SetCameraFollowTarget(GameObject target) {
      if (Camera.main.TryGetComponent<CameraMover>(out var cameraMover)) {
        cameraMover.SetTarget(target);
      }
    }
  }
}