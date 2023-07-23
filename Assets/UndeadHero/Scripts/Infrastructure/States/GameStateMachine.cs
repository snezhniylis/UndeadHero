using System;
using System.Collections.Generic;

namespace UndeadHero.Infrastructure.States {
  public class GameStateMachine {
    private readonly Dictionary<Type, IStateBase> _states;
    private IStateBase _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen) {
      _states = new Dictionary<Type, IStateBase> {
        [typeof(StateBootstrap)] = new StateBootstrap(this, sceneLoader),
        [typeof(StateLoadLevel)] = new StateLoadLevel(this, sceneLoader, loadingScreen),
        [typeof(StateGameLoop)] = new StateGameLoop(this)
      };
    }

    public void Enter<TState>() where TState : class, IState =>
      ChangeState<TState>().Enter();

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IStatePayloaded<TPayload> =>
      ChangeState<TState>().Enter(payload);

    private TState ChangeState<TState>() where TState : class, IStateBase {
      _activeState?.Exit();
      TState state = GetState<TState>();
      _activeState = state;
      return state;
    }

    private TState GetState<TState>() where TState : class, IStateBase {
      return _states[typeof(TState)] as TState;
    }
  }
}