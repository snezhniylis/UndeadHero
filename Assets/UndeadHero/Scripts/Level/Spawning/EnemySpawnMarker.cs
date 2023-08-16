using System;
using UndeadHero.StaticData;
using UnityEngine;

namespace UndeadHero.Level.Spawning {
  public class EnemySpawnMarker : MonoBehaviour {
    [field: SerializeField] public EnemyTypeId EnemyTypeId { get; private set; }

    [field: SerializeField] public string SpawnerId { get; private set; }

    public void GenerateNewSpawnerId() =>
      SpawnerId = Guid.NewGuid().ToString("N");
  }
}
