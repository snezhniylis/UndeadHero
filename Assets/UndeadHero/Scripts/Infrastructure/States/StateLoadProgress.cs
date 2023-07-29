using UndeadHero.Infrastructure.Services.PersistentProgress;

namespace UndeadHero.Infrastructure.States {
  public class StateLoadProgress : IState {
    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;

    public StateLoadProgress(GameStateMachine gameStateMachine, IPersistentProgressService progressService) {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
    }

    public void Enter() {
      _progressService.InitializeProgress();
      _gameStateMachine.Enter<StateLoadLevel, string>(_progressService.Progress.WorldData.Level);
    }

    public void Exit() {
    }
  }
}
