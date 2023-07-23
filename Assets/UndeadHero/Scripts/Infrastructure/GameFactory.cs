using UnityEngine;
using UndeadHero.Infrastructure.AssetManagement;

namespace UndeadHero.Infrastructure {
  public class GameFactory : IGameFactory {
    private const string DogePrefabPath = "Characters/Playable/Doge/Doge";
    private const string HudPrefabPath = "UI/HUD";
    private readonly IAssetProvider _assetProvider;

    public GameFactory(IAssetProvider assetProvider) {
      _assetProvider = assetProvider;
    }

    public GameObject CreateHero(GameObject spawnPoint) =>
      _assetProvider.Instantiate(DogePrefabPath, spawnPoint.transform.position, spawnPoint.transform.rotation);

    public void CreateHud() =>
      _assetProvider.Instantiate(HudPrefabPath);
  }
}