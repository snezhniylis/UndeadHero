using System;
using UndeadHero.Services.Input;
using UnityEngine;

namespace UndeadHero.Infrastructure {
  public class StateBootstrap : IState {
    private const string EntrySceneName = "Entry";

    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public StateBootstrap(GameStateMachine stateMachine, SceneLoader sceneLoader) {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter() {
      Game.InputService = InitializeInputService();
    }

    private void EntrySceneLoaded() { }

    public void Exit() { }

    private static IInputService InitializeInputService() {
      return Application.isEditor ? new DebugInputService() : new MobileInputService();
    }
  }
}