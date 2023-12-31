using UnityEngine;

namespace UndeadHero.Infrastructure.Services.AssetManagement {
  public interface IAssetProvider {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 position, Quaternion rotation);
    GameObject Instantiate(string path, Transform parent);
  }
}
