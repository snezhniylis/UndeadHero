using System;
using UnityEngine;

namespace UndeadHero.StaticData.Serializable {
  [Serializable]
  public class EnemySpawnerData {
    public string SpawnerId;
    public EnemyTypeId EnemyTypeId;
    public Vector3 Position;
  }
}
