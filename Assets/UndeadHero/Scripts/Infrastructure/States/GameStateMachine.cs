using System;
using VContainer;
using VContainer.Unity;

namespace UndeadHero.Infrastructure.States {
  public class GameStateMachine {
    private readonly LifetimeScope _gameDiScope;

    private LifetimeScope _sceneDiScope;
    private IStateBase _activeState;

    public GameStateMachine(LifetimeScope gameDiScope) {
      _gameDiScope = gameDiScope;

      InitializeSceneDiScope();
    }

    public void Enter<TState>() where TState : class, IState =>
      ChangeState<TState>().Enter();

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IStatePayloaded<TPayload> =>
      ChangeState<TState>().Enter(payload);

    private TState ChangeState<TState>() where TState : class, IStateBase {
      _activeState?.Exit();
      var state = _sceneDiScope.Container.Resolve<TState>();
      _activeState = state;
      return state;
    }

    public void InitializeSceneDiScope(Action<IContainerBuilder> buildScope = null) {
      buildScope ??= (_) => { };
      _sceneDiScope = _gameDiScope.CreateChild(buildScope);
    }

    public void DestroySceneDiScope() {
      _sceneDiScope.Dispose();
      _sceneDiScope = null;
    }
  }
}
