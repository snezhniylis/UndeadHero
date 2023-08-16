using System.Collections.Generic;
using System.Linq;
using UndeadHero.Infrastructure.Services.AssetManagement;
using UndeadHero.StaticData;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.StaticDataManagement {
  public class StaticDataProvider : IStaticDataProvider {
    private readonly HeroStaticData _heroData;
    private readonly Dictionary<EnemyTypeId, EnemyStaticData> _enemyData;
    private readonly Dictionary<string, LevelStaticData> _levelData;

    public StaticDataProvider() {
      _heroData = Resources.Load<HeroStaticData>(AssetPaths.HeroStaticData);
      _enemyData = Resources.LoadAll<EnemyStaticData>(AssetPaths.EnemiesStaticData).ToDictionary(x => x.EnemyTypeId, x => x);
      _levelData = Resources.LoadAll<LevelStaticData>(AssetPaths.LevelsStaticData).ToDictionary(x => x.LevelName, x => x);
    }

    public HeroStaticData GetHeroData() =>
      _heroData;

    public EnemyStaticData GetEnemyData(EnemyTypeId typeId) =>
      _enemyData.TryGetValue(typeId, out EnemyStaticData enemyData) ? enemyData : null;

    public LevelStaticData GetLevelData(string levelName) =>
      _levelData.TryGetValue(levelName, out LevelStaticData levelData) ? levelData : null;
  }
}
