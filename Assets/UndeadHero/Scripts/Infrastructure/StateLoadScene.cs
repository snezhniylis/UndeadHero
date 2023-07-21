using UnityEngine;
using UndeadHero.CameraLogic;

namespace UndeadHero.Infrastructure {
  class StateLoadScene : IStatePayloaded<string> {
    private const string HudPrefabPath = "UI/HUD";
    private const string DogePrefabPath = "Characters/Playable/Doge/Doge";

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
      GameObject doge = InstantiatePrefab(DogePrefabPath);
      SetCameraFollowTarget(doge);
    }

    private static GameObject InstantiatePrefab(string path) {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }

    private void SetCameraFollowTarget(GameObject target) {
      if (Camera.main.TryGetComponent<CameraMover>(out var cameraMover)) {
        cameraMover.SetTarget(target);
      }
    }
  }
}