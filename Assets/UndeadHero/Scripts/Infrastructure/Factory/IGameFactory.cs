using UnityEngine;

namespace UndeadHero.Infrastructure.Factory {
  public interface IGameFactory {
    GameObject CreateHero(GameObject spawnPoint);
    void CreateHud();
  }
}