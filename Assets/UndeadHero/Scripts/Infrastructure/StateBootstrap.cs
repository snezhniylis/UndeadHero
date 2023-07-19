using UndeadHero.Services.Input;
using UnityEngine;

namespace UndeadHero.Infrastructure {
  public class StateBootstrap : IState {
    private GameStateMachine _stateMachine;

    public StateBootstrap(GameStateMachine stateMachine) {
      _stateMachine = stateMachine;
    }

    public void Enter() {
      Game.InputService = InitializeInputService();
    }

    public void Exit() { }

    private static IInputService InitializeInputService() {
      return Application.isEditor ? new DebugInputService() : new MobileInputService();
    }
  }
}