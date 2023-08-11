using UnityEngine;

namespace UndeadHero.Infrastructure.Services.AssetManagement {
  public interface IAssetProvider : IService {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 position, Quaternion rotation);
  }
}
