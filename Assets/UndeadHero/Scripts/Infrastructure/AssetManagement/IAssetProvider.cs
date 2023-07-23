using UnityEngine;

namespace UndeadHero.Infrastructure.AssetManagement {
  public interface IAssetProvider {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 position, Quaternion rotation);
  }
}