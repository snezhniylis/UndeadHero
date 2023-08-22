using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.AssetManagement;
using UndeadHero.Infrastructure.Services.StaticDataManagement;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.StaticData.Views;
using UndeadHero.UI.Elements;
using UndeadHero.UI.Views;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.UiFactory {
  public class UiFactory : IUiFactory {
    private readonly IAssetProvider _assetProvider;
    private readonly IStaticDataProvider _staticDataProvider;
    private readonly IViewManager _viewManager;

    public UiFactory(IAssetProvider assetProvider, IStaticDataProvider staticDataProvider) {
      _assetProvider = assetProvider;
      _staticDataProvider = staticDataProvider;
    }

    public Transform CreateUiRoot() =>
      InstantiateByPath(AssetPaths.UiRoot).transform;

    public GameObject CreateEventButton(GameEvent gameEvent, Transform parent, IViewManager viewManager, HeroInventory heroInventory) {
      GameObject button = InstantiateByPath(AssetPaths.HudEventButton, parent);

      button.GetComponent<OpenEventViewButton>().Initialize(gameEvent, viewManager, heroInventory);

      return button;
    }

    public EventView CreateEventView(ViewId viewId, Transform parent, ViewManager viewManager, GameEvent gameEvent, HeroInventory heroInventory) {
      ViewStaticData viewData = _staticDataProvider.GetViewData(viewId);

      var view = InstantiatePrefab(viewData.Prefab, parent).GetComponent<EventView>();

      view.Initialize(
        viewManager,
        gameEvent,
        heroInventory
      );

      return view;
    }

    private GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation) =>
      Object.Instantiate(prefab, position, rotation);

    private GameObject InstantiatePrefab(GameObject prefab, Transform parent) =>
      Object.Instantiate(prefab, parent);

    private GameObject InstantiateByPath(string path) =>
      _assetProvider.Instantiate(path);

    private GameObject InstantiateByPath(string path, Transform parent) =>
      _assetProvider.Instantiate(path, parent);
  }
}
