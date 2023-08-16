using UndeadHero.StaticData;

namespace UndeadHero.Infrastructure.Services.StaticDataManagement {
  public interface IStaticDataProvider : IService {
    HeroStaticData GetHeroData();
    EnemyStaticData GetEnemyData(EnemyTypeId typeId);
    LevelStaticData GetLevelData(string levelName);
  }
}
