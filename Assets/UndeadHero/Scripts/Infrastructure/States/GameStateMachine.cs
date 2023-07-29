using System;
using System.Collections.Generic;
using UndeadHero.Infrastructure.Factory;
using UndeadHero.Infrastructure.Services;
using UndeadHero.Infrastructure.Services.PersistentProgress;

namespace UndeadHero.Infrastructure.States {
  public class GameStateMachine {
    private readonly Dictionary<Type, IStateBase> _states;
    private IStateBase _activeState;

    public GameStateMachine(SceneLoader sceneLoader, LoadingScreen loadingScreen, GameServices gameServices) {
      _states = new Dictionary<Type, IStateBase> {
        [typeof(StateBootstrap)] = new StateBootstrap(this, sceneLoader, gameServices),
        [typeof(StateLoadProgress)] = new StateLoadProgress(this, gameServices.Single<IPersistentProgressService>()),
        [typeof(StateLoadLevel)] = new StateLoadLevel(this, sceneLoader, loadingScreen, gameServices.Single<IGameFactory>(), gameServices.Single<IPersistentProgressService>()),
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
