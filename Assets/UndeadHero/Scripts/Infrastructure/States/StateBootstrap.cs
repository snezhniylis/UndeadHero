using UnityEngine;
using UndeadHero.Infrastructure.Services;
using UndeadHero.Infrastructure.Services.Input;
using UndeadHero.Infrastructure.Factory;
using UndeadHero.Infrastructure.AssetManagement;

namespace UndeadHero.Infrastructure.States {
  public class StateBootstrap : IState {
    private const string EntrySceneName = "Entry";
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    private readonly GameServices _gameServices;

    public StateBootstrap(GameStateMachine stateMachine, SceneLoader sceneLoader, GameServices gameServices) {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _gameServices = gameServices;

      RegisterGameServices();
    }

    public void Enter() =>
      _sceneLoader.Load(EntrySceneName, OnEntrySceneLoaded);

    public void Exit() { }

    private void OnEntrySceneLoaded() {
      _stateMachine.Enter<StateLoadProgress>();
    }

    private void RegisterGameServices() {
      _gameServices.RegisterSingle<IInputService>(InitializeInputService());
      _gameServices.RegisterSingle<IAssetProvider>(new AssetProvider());
      _gameServices.RegisterSingle<IGameFactory>(new GameFactory(GameServices.Container.Single<IAssetProvider>()));
    }

    private static IInputService InitializeInputService() {
      return Application.isEditor ? new DebugInputService() : new MobileInputService();
    }
  }
}
