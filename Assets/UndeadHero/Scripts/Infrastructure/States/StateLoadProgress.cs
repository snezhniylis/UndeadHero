using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.Events;
using UndeadHero.Infrastructure.Services.PersistentProgress;

namespace UndeadHero.Infrastructure.States {
  public class StateLoadProgress : IState {
    private const string FirstPlayableSceneName = "Cemetery";

    private readonly GameStateMachine _gameStateMachine;
    private readonly IPersistentProgressService _progressService;
    private readonly IEventRegistry _eventRegistry;

    public StateLoadProgress(GameStateMachine gameStateMachine, IPersistentProgressService progressService, IEventRegistry eventRegistry) {
      _gameStateMachine = gameStateMachine;
      _progressService = progressService;
      _eventRegistry = eventRegistry;
    }

    public void Enter() {
      PlayerProgress progress = _progressService.LoadSavedProgress();

      _eventRegistry.LoadGameEvents(progress);

      string levelToLoad = progress.CurrentLevel == null ? FirstPlayableSceneName : progress.CurrentLevel.Name;
      _gameStateMachine.Enter<StateLoadLevel, string>(levelToLoad);
    }

    public void Exit() { }
  }
}
