using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.Ads;
using UndeadHero.Infrastructure.Services.AssetManagement;
using UndeadHero.Infrastructure.Services.Events;
using UndeadHero.Infrastructure.Services.SceneObjectsRegistry;
using UndeadHero.Infrastructure.Services.StaticDataManagement;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.StaticData.Views;
using UndeadHero.UI.Elements;
using UndeadHero.UI.Views;
using UnityEngine;
using VContainer;

namespace UndeadHero.Infrastructure.Services.UiFactory {
  public class UiFactory : IUiFactory {
    private readonly IObjectResolver _sceneDiContainer;

    private readonly IAssetProvider _assetProvider;
    private readonly IStaticDataProvider _staticDataProvider;
    private readonly ISceneObjectsRegistry _sceneObjects;
    private readonly IEventRegistry _eventRegistry;
    private readonly IAdService _adService;

    public UiFactory(IObjectResolver sceneDiContainer, IAssetProvider assetProvider, IStaticDataProvider staticDataProvider, ISceneObjectsRegistry sceneObjectsRegistry, IEventRegistry eventRegistry, IAdService adService) {
      // `UiFactory` requires `ViewManager` to be passed as a dependency to UI elements,
      // meanwhile `ViewManager` already utilizes `UiFactory` to create non-existent views.
      // The somewhat hacky solution to this circular dependency is to use the DI container
      // to get the ViewManager instance when instantiating UI elements.
      // I could just use Container.Inject(view), but then I'll have to sacrifice some
      // verbosity in views' constructors, because I won't be able to pass objects like
      // HeroInventory directly.
      _sceneDiContainer = sceneDiContainer;

      _assetProvider = assetProvider;
      _staticDataProvider = staticDataProvider;
      _sceneObjects = sceneObjectsRegistry;
      _eventRegistry = eventRegistry;
      _adService = adService;
    }

    public Transform CreateUiRoot() =>
      InstantiateByPath(AssetPaths.UiRoot).transform;

    public GameObject CreateEventButton(GameEvent gameEvent, Transform parent) {
      GameObject button = InstantiateByPath(AssetPaths.HudEventButton, parent);

      button.GetComponent<OpenEventViewButton>().Initialize(
        gameEvent,
        _sceneDiContainer.Resolve<IViewManager>()
      );

      return button;
    }

    public View CreateView(ViewId viewId, Transform parent) {
      ViewStaticData viewData = _staticDataProvider.GetViewData(viewId);
      GameObject view = InstantiatePrefab(viewData.Prefab, parent);

      switch (viewId) {
        case ViewId.HalloweenShop:
          InitializeHalloweenShopView(view);
          break;
        case ViewId.DailyAd:
          InitializeDailyAdView(view);
          break;
      }

      return view.GetComponent<View>();
    }

    private void InitializeHalloweenShopView(GameObject view) =>
      view.GetComponent<HalloweenShopView>().Initialize(
        _sceneDiContainer.Resolve<IViewManager>(),
        _sceneObjects.Hero.GetComponent<HeroInventory>()
      );

    private void InitializeDailyAdView(GameObject view) =>
      view.GetComponent<DailyAdView>().Initialize(
        _sceneDiContainer.Resolve<IViewManager>(),
        _eventRegistry,
        _adService,
        _sceneObjects.Hero.GetComponent<HeroInventory>()
      );

    private GameObject InstantiatePrefab(GameObject prefab, Transform parent) =>
      Object.Instantiate(prefab, parent);

    private GameObject InstantiateByPath(string path) =>
      _assetProvider.Instantiate(path);

    private GameObject InstantiateByPath(string path, Transform parent) =>
      _assetProvider.Instantiate(path, parent);
  }
}
