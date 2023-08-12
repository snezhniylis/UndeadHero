using System.Collections.Generic;
using System.Linq;
using UndeadHero.Infrastructure.Services.AssetManagement;
using UndeadHero.StaticData;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.StaticDataManagement {
  public class StaticDataProvider : IStaticDataProvider {
    private readonly HeroStaticData _heroData;
    private readonly Dictionary<EnemyTypeId, EnemyStaticData> _enemyData;

    public StaticDataProvider() {
      _heroData = Resources.Load<HeroStaticData>(AssetPaths.HeroStaticData);
      _enemyData = Resources.LoadAll<EnemyStaticData>(AssetPaths.EnemiesStaticData).ToDictionary(x => x.EnemyTypeId, x => x);
    }

    public HeroStaticData GetHeroData() =>
      _heroData;

    public EnemyStaticData GetEnemyData(EnemyTypeId typeId) =>
      _enemyData.TryGetValue(typeId, out EnemyStaticData enemyData) ? enemyData : null;
  }
}
