using UndeadHero.StaticData.Enemies;
using UndeadHero.UI.Hud;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.Factory {
  public interface IGameFactory {
    GameObject CreateHero(Vector3 position);
    GameObject CreateEnemy(EnemyTypeId typeId, Vector3 position, Quaternion rotation);
    GameObject CreateEnemyLootContainer(Vector3 position);
    void CreateEnemySpawner(Vector3 position, string spawnerId, EnemyTypeId enemyId);
    PlayerHud CreateHud();
  }
}
