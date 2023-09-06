using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.AssetManagement;
using UndeadHero.Infrastructure.Services.StaticDataManagement;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.StaticData.Views;
using UndeadHero.UI.Elements;
using UndeadHero.UI.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace UndeadHero.Infrastructure.Services.UiFactory {
  public class UiFactory : IUiFactory {
    private readonly IObjectResolver _sceneDiContainer;

    private readonly IAssetProvider _assetProvider;
    private readonly IStaticDataProvider _staticDataProvider;

    public UiFactory(IObjectResolver sceneDiContainer, IAssetProvider assetProvider, IStaticDataProvider staticDataProvider) {
      _sceneDiContainer = sceneDiContainer;

      _assetProvider = assetProvider;
      _staticDataProvider = staticDataProvider;
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

      GameObject view = InstantiateAndInject(viewData.Prefab, parent);

      return view.GetComponent<View>();
    }

    private GameObject InstantiateAndInject(GameObject prefab, Transform parent) =>
      _sceneDiContainer.Instantiate(prefab, parent);

    private GameObject InstantiateByPath(string path) =>
      _assetProvider.Instantiate(path);

    private GameObject InstantiateByPath(string path, Transform parent) =>
      _assetProvider.Instantiate(path, parent);
  }
}
