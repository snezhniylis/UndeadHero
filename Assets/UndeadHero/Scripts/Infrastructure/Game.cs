using UndeadHero.Infrastructure.Services.AssetManagement;
using UndeadHero.Infrastructure.Services.Coroutines;
using UndeadHero.Infrastructure.Services.Input;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.Infrastructure.Services.Random;
using UndeadHero.Infrastructure.Services.SaveManagement;
using UndeadHero.Infrastructure.Services.SceneLoading;
using UndeadHero.Infrastructure.Services.StaticDataManagement;
using UndeadHero.Infrastructure.States;
using UndeadHero.UI.LoadingScreen;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UndeadHero.Infrastructure {
  public class Game : LifetimeScope {
    [SerializeField] private LoadingScreen _loadingScreenPrefab;

    private GameStateMachine _stateMachine;

    protected override void Configure(IContainerBuilder builder) {
      base.Configure(builder);

      RegisterGlobalGameServices(builder);
    }

    private void Start() {
      DontDestroyOnLoad(this);

      _stateMachine = Container.Resolve<GameStateMachine>();
      _stateMachine.Enter<StateLoadProgress>();
    }

    private void RegisterGlobalGameServices(IContainerBuilder builder) {
      builder.RegisterComponentInNewPrefab(_loadingScreenPrefab, Lifetime.Singleton);

      builder.RegisterComponent<ICoroutineRunner>(gameObject.AddComponent<CoroutineRunner>());

      builder.Register<GameStateMachine>(Lifetime.Singleton);

      builder.Register<StateLoadProgress>(Lifetime.Transient);
      builder.Register<StateLoadLevel>(Lifetime.Transient);
      builder.Register<StateInitializeLevel>(Lifetime.Transient);
      builder.Register<StateGameLoop>(Lifetime.Transient);

      if (Application.isEditor) {
        builder.Register<IInputService, DebugInputService>(Lifetime.Singleton);
      }
      else {
        builder.Register<IInputService, MobileInputService>(Lifetime.Singleton);
      }

      builder.Register<ISceneLoader, SceneLoader>(Lifetime.Singleton);
      builder.Register<IStaticDataProvider, StaticDataProvider>(Lifetime.Singleton);
      builder.Register<ISaveManager, SaveManager>(Lifetime.Singleton);
      builder.Register<IPersistentProgressService, PersistentProgressService>(Lifetime.Singleton);
      builder.Register<IAssetProvider, AssetProvider>(Lifetime.Singleton);
      builder.Register<IRandomizer, Randomizer>(Lifetime.Singleton);
    }
  }
}
