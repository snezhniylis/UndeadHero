using UnityEngine;
using UndeadHero.CameraLogic;

namespace UndeadHero.Infrastructure.Factory {
  public interface IGameFactory {
    GameObject CreateHero(GameObject spawnPoint);
    void CreateHud();
  }
}