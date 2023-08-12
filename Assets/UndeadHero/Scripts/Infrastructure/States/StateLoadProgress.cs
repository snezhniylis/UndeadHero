using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.PersistentProgress;

namespace UndeadHero.Infrastructure.States {
  public class StateLoadProgress : IState {
    private const string FirstPlayableSceneName = "Cemetery";

    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;

    public StateLoadProgress(GameStateMachine gameStateMachine, IPersistentProgressService progressService) {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
    }

    public void Enter() {
      PlayerProgress progress = _progressService.LoadSavedProgress();

      string levelToLoad = progress == null ? FirstPlayableSceneName : progress.WorldData.Level;
      _gameStateMachine.Enter<StateLoadLevel, string>(levelToLoad);
    }

    public void Exit() { }
  }
}
