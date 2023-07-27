namespace UndeadHero.Infrastructure.States {
  class StateGameLoop : IState {
    private readonly GameStateMachine _stateMachine;

    public StateGameLoop(GameStateMachine stateMachine) {
      _stateMachine = stateMachine;
    }

    public void Enter() { }

    public void Exit() { }
  }
}
