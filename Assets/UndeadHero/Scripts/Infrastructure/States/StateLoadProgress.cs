using UndeadHero.Infrastructure.Services.PersistentProgress;

namespace UndeadHero.Infrastructure.States {
  public class StateLoadProgress : IState {
    private const string PlayableSceneName = "Cemetery";
    private readonly GameStateMachine _gameStateMachine;

    public StateLoadProgress(GameStateMachine gameStateMachine) {
      _gameStateMachine = gameStateMachine;
    }

    public void Enter() {
      _gameStateMachine.Enter<StateLoadLevel, string>(PlayableSceneName);
    }

    public void Exit() {
    }
  }
}
