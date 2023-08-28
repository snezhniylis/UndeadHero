using UndeadHero.Infrastructure.Services.Factory;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.Infrastructure.Services.SceneLoading;
using UndeadHero.Infrastructure.Services.SceneObjectsRegistry;
using UndeadHero.Infrastructure.Services.UiFactory;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.UI.LoadingScreen;
using VContainer;

namespace UndeadHero.Infrastructure.States {
  public class StateLoadLevel : IStatePayloaded<string> {
    private readonly GameStateMachine _stateMachine;
    private readonly LoadingScreen _loadingScreen;
    private readonly ISceneLoader _sceneLoader;
    private readonly IPersistentProgressService _progressService;

    public StateLoadLevel(GameStateMachine stateMachine, LoadingScreen loadingScreen, ISceneLoader sceneLoader, IPersistentProgressService progressService) {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
      _loadingScreen = loadingScreen;
      _progressService = progressService;
    }

    public void Enter(string sceneName) {
      _loadingScreen.Show();
      _progressService.CleanUp();
      _stateMachine.DestroySceneDiScope();
      _sceneLoader.Load(sceneName, OnSceneLoaded);
    }

    public void Exit() { }

    private void OnSceneLoaded() {
      _stateMachine.InitializeSceneDiScope(BuildSceneDiScope);
      _stateMachine.Enter<StateInitializeLevel>();
    }

    private static void BuildSceneDiScope(IContainerBuilder builder) {
      builder.Register<ISceneObjectsRegistry, SceneObjectsRegistry>(Lifetime.Scoped);
      builder.Register<IGameFactory, GameFactory>(Lifetime.Scoped);
      builder.Register<IUiFactory, UiFactory>(Lifetime.Scoped);
      builder.Register<IViewManager, ViewManager>(Lifetime.Scoped);
    }
  }
}
