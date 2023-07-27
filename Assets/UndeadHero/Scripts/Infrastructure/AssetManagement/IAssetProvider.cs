using UnityEngine;
using UndeadHero.Infrastructure.Services;

namespace UndeadHero.Infrastructure.AssetManagement {
  public interface IAssetProvider : IService {
    GameObject Instantiate(string path);
    GameObject Instantiate(string path, Vector3 position, Quaternion rotation);
  }
}
