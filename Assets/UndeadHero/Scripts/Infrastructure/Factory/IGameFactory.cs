using UnityEngine;
using UndeadHero.Infrastructure.Services;

namespace UndeadHero.Infrastructure.Factory {
  public interface IGameFactory : IService {
    GameObject CreateHero(GameObject spawnPoint);
    void CreateHud();
  }
}