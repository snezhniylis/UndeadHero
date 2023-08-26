using System.Collections.Generic;
using UndeadHero.StaticData.Enemies;
using UndeadHero.StaticData.Events;
using UndeadHero.StaticData.Heroes;
using UndeadHero.StaticData.Levels;
using UndeadHero.StaticData.Views;
using UndeadHero.UI.Views;

namespace UndeadHero.Infrastructure.Services.StaticDataManagement {
  public interface IStaticDataProvider {
    HeroStaticData GetHeroData();
    EnemyStaticData GetEnemyData(EnemyTypeId typeId);
    LevelStaticData GetLevelData(string levelName);
    ViewStaticData GetViewData(ViewId viewId);
    IEnumerable<EventStaticData> GetAllEventsData();
  }
}
