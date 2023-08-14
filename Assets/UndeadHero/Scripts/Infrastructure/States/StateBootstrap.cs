using UnityEngine;
using UndeadHero.Infrastructure.Services;
using UndeadHero.Infrastructure.Services.AssetManagement;
using UndeadHero.Infrastructure.Services.Factory;
using UndeadHero.Infrastructure.Services.Input;
using UndeadHero.Infrastructure.Services.SaveManagement;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.Infrastructure.Services.Random;
using UndeadHero.Infrastructure.Services.StaticDataManagement;

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
      _gameServices.RegisterSingle<IStaticDataProvider>(new StaticDataProvider());
      _gameServices.RegisterSingle<ISaveManager>(new SaveManager());
      _gameServices.RegisterSingle<IPersistentProgressService>(new PersistentProgressService(_gameServices.Single<ISaveManager>()));
      _gameServices.RegisterSingle<IAssetProvider>(new AssetProvider());
      _gameServices.RegisterSingle<IRandomizer>(new Randomizer());
      _gameServices.RegisterSingle<IGameFactory>(new GameFactory(_gameServices.Single<IAssetProvider>(), _gameServices.Single<IPersistentProgressService>(), _gameServices.Single<IStaticDataProvider>(), _gameServices.Single<IRandomizer>()));
    }

    private static IInputService InitializeInputService() =>
      Application.isEditor ? new DebugInputService() : new MobileInputService();
  }
}
