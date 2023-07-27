using UnityEngine;
using UndeadHero.Infrastructure.AssetManagement;

namespace UndeadHero.Infrastructure.Factory {
  public class GameFactory : IGameFactory {
    private readonly IAssetProvider _assetProvider;

    public GameFactory(IAssetProvider assetProvider) {
      _assetProvider = assetProvider;
    }

    public GameObject CreateHero(GameObject spawnPoint) =>
      _assetProvider.Instantiate(AssetPaths.Doge, spawnPoint.transform.position, spawnPoint.transform.rotation);

    public void CreateHud() =>
      _assetProvider.Instantiate(AssetPaths.Hud);
  }
}
