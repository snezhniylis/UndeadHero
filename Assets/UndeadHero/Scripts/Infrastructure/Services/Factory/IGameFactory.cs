using UndeadHero.StaticData;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.Factory {
  public interface IGameFactory : IService {
    GameObject CreateHero(Vector3 position, Quaternion rotation);
    GameObject CreateEnemy(EnemyTypeId typeId, Vector3 position, Quaternion rotation, GameObject hero);
    void CreateHud(GameObject hero);
  }
}
