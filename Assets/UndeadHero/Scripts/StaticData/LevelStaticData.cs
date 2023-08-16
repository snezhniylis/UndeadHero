using System.Collections.Generic;
using UndeadHero.StaticData.Serializable;
using UnityEngine;

namespace UndeadHero.StaticData {
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level", order = 0)]
  public class LevelStaticData : ScriptableObject {
    public string LevelName;
    public List<EnemySpawnerData> EnemySpawners;
  }
}
