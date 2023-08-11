using UnityEngine;

namespace UndeadHero.Infrastructure.Services.AssetManagement {
  public class AssetProvider : IAssetProvider {
    public GameObject Instantiate(string path) =>
      Instantiate(path, Vector3.zero, Quaternion.identity);

    public GameObject Instantiate(string path, Vector3 position, Quaternion rotation) {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, position, rotation);
    }
  }
}
