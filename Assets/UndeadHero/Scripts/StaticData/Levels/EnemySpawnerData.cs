using System;
using UndeadHero.StaticData.Enemies;
using UnityEngine;

namespace UndeadHero.StaticData.Levels {
  [Serializable]
  public class EnemySpawnerData {
    public string SpawnerId;
    public EnemyTypeId EnemyTypeId;
    public Vector3 Position;
  }
}
