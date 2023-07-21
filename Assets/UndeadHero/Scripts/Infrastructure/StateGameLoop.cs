namespace UndeadHero.Infrastructure {
  class StateGameLoop : IState {
    private readonly GameStateMachine _gameStateMachine;

    public StateGameLoop(GameStateMachine gameStateMachine) {
      _gameStateMachine = gameStateMachine;
    }

    public void Enter() { }

    public void Exit() { }
  }
}