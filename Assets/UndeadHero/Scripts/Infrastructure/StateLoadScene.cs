namespace UndeadHero.Infrastructure {
  class StateLoadScene : IStatePayloaded<string> {
    private readonly GameStateMachine _gameStateMachine;
    private readonly SceneLoader _sceneLoader;

    public StateLoadScene(GameStateMachine gameStateMachine, SceneLoader sceneLoader) {
      _gameStateMachine = gameStateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter(string sceneName) =>
      _sceneLoader.Load(sceneName);

    public void Exit() { }
  }
}