using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.AssetManagement;
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
    private readonly ISceneObjectsRegistry _sceneObjectsRegistry;

    public UiFactory(IObjectResolver sceneDiContainer, IAssetProvider assetProvider, IStaticDataProvider staticDataProvider, ISceneObjectsRegistry sceneObjectsRegistry) {
      // `UiFactory` requires `ViewManager` to be passed as a dependency to UI elements,
      // meanwhile `ViewManager` already utilizes `UiFactory` to create non-existent views.
      // The somewhat hacky solution to this circular dependency is to use the DI container
      // to get the ViewManager instance when instantiating UI elements.
      _sceneDiContainer = sceneDiContainer;

      _assetProvider = assetProvider;
      _staticDataProvider = staticDataProvider;
      _sceneObjectsRegistry = sceneObjectsRegistry;
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
      }

      return view.GetComponent<View>();
    }

    private void InitializeHalloweenShopView(GameObject view) =>
      view.GetComponent<HalloweenShopView>().Initialize(
        _sceneDiContainer.Resolve<IViewManager>(),
        _sceneObjectsRegistry.Hero.GetComponent<HeroInventory>()
      );

    private GameObject InstantiatePrefab(GameObject prefab, Transform parent) =>
      Object.Instantiate(prefab, parent);

    private GameObject InstantiateByPath(string path) =>
      _assetProvider.Instantiate(path);

    private GameObject InstantiateByPath(string path, Transform parent) =>
      _assetProvider.Instantiate(path, parent);
  }
}
