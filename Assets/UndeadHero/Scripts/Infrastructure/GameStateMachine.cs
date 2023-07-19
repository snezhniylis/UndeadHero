using System;
using System.Collections.Generic;

namespace UndeadHero.Infrastructure {
  public class GameStateMachine {
    private readonly Dictionary<Type, IState> _states;
    private IState _activeState;

    public GameStateMachine(SceneLoader sceneLoader) {
      _states = new Dictionary<Type, IState> {
        [typeof(StateBootstrap)] = new StateBootstrap(this, sceneLoader)
      };
    }

    public void Enter<TState>() where TState : IState {
      _activeState?.Exit();
      _activeState = _states[typeof(TState)];
      _activeState.Enter();
    }
  }
}