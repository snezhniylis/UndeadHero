using UnityEngine;
using UndeadHero.CameraLogic;

namespace UndeadHero.Infrastructure {
  public interface IGameFactory {
    GameObject CreateHero(GameObject spawnPoint);
    void CreateHud();
  }
}