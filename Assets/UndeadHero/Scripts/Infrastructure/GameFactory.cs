using UnityEngine;

namespace UndeadHero.Infrastructure {
  public class GameFactory : IGameFactory {
    private const string DogePrefabPath = "Characters/Playable/Doge/Doge";
    private const string HudPrefabPath = "UI/HUD";

    public GameObject CreateHero(GameObject spawnPoint) =>
      Instantiate(DogePrefabPath, spawnPoint.transform.position, spawnPoint.transform.rotation);

    public void CreateHud() =>
      Instantiate(HudPrefabPath);

    private GameObject Instantiate(string path) =>
      Instantiate(path, Vector3.zero, Quaternion.identity);

    private GameObject Instantiate(string path, Vector3 position, Quaternion rotation) {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, position, rotation);
    }
  }
}