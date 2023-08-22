using UnityEngine;

namespace UndeadHero.Infrastructure.Services.AssetManagement {
  public class AssetProvider : IAssetProvider {
    public GameObject Instantiate(string path) =>
      Instantiate(path, Vector3.zero, Quaternion.identity);

    public GameObject Instantiate(string path, Vector3 position, Quaternion rotation) =>
      Object.Instantiate(LoadPrefab(path), position, rotation);

    public GameObject Instantiate(string path, Transform parent) =>
      Object.Instantiate(LoadPrefab(path), parent);

    private static GameObject LoadPrefab(string path) =>
      Resources.Load<GameObject>(path);
  }
}
