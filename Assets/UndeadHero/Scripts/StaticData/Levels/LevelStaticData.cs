using System.Collections.Generic;
using UnityEngine;

namespace UndeadHero.StaticData.Levels {
  [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level", order = 0)]
  public class LevelStaticData : ScriptableObject {
    public string LevelName;
    public List<EnemySpawnerData> EnemySpawners;
  }
}
