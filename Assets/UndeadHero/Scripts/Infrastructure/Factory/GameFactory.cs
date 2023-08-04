using System;
using UnityEngine;
using UndeadHero.Infrastructure.AssetManagement;
using UndeadHero.Infrastructure.Services.PersistentProgress;

namespace UndeadHero.Infrastructure.Factory {
  public class GameFactory : IGameFactory {
    private readonly IAssetProvider _assetProvider;
    private readonly IPersistentProgressService _persistentProgress;

    public GameFactory(IAssetProvider assetProvider, IPersistentProgressService persistentProgress) {
      _assetProvider = assetProvider;
      _persistentProgress = persistentProgress;
    }

    public GameObject HeroGameObject { get; private set; }
    public event Action OnHeroCreated;

    public GameObject CreateHero(GameObject spawnPoint) {
      HeroGameObject = Instantiate(AssetPaths.Doge, spawnPoint.transform.position, spawnPoint.transform.rotation);
      OnHeroCreated?.Invoke();
      return HeroGameObject;
    }

    public GameObject CreateHud() =>
      Instantiate(AssetPaths.Hud);

    private GameObject Instantiate(string path) {
      GameObject gameObject = _assetProvider.Instantiate(path);
      BindToPersistentProgress(gameObject);

      return gameObject;
    }

    private GameObject Instantiate(string path, Vector3 position, Quaternion rotation) {
      GameObject gameObject = _assetProvider.Instantiate(path, position, rotation);
      BindToPersistentProgress(gameObject);

      return gameObject;
    }

    private void BindToPersistentProgress(GameObject gameObject) {
      foreach (IPersistentProgressReader progressReader in gameObject.GetComponentsInChildren<IPersistentProgressReader>()) {
        _persistentProgress.AddSubscriber(progressReader);
      }
    }
  }
}
