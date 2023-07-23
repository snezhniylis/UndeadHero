using UndeadHero.Services.Input;
using UnityEngine;

namespace UndeadHero.Infrastructure.States {
  public class StateBootstrap : IState {
    private const string EntrySceneName = "Entry";
    private const string PlayableSceneName = "Cemetery";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public StateBootstrap(GameStateMachine stateMachine, SceneLoader sceneLoader) {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter() =>
      _sceneLoader.Load(EntrySceneName, OnEntrySceneLoaded);

    public void Exit() { }

    private void OnEntrySceneLoaded() {
      InitializeServices();
      _stateMachine.Enter<StateLoadLevel, string>(PlayableSceneName);
    }

    private static void InitializeServices() {
      Game.InputService = InitializeInputService();
    }

    private static IInputService InitializeInputService() {
      return Application.isEditor ? new DebugInputService() : new MobileInputService();
    }
  }
}