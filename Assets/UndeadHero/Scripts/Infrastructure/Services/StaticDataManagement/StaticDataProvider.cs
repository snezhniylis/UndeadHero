using System.Collections.Generic;
using System.Linq;
using UndeadHero.Infrastructure.Services.AssetManagement;
using UndeadHero.StaticData.Enemies;
using UndeadHero.StaticData.Events;
using UndeadHero.StaticData.Heroes;
using UndeadHero.StaticData.Levels;
using UndeadHero.StaticData.Views;
using UndeadHero.UI.Views;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.StaticDataManagement {
  public class StaticDataProvider : IStaticDataProvider {
    private readonly HeroStaticData _heroData;
    private readonly Dictionary<EnemyTypeId, EnemyStaticData> _enemyData;
    private readonly Dictionary<string, LevelStaticData> _levelData;
    private readonly Dictionary<string, EventStaticData> _eventData;
    private readonly Dictionary<ViewId, ViewStaticData> _viewData;

    public StaticDataProvider() {
      _heroData = Resources.Load<HeroStaticData>(AssetPaths.HeroStaticData);
      _enemyData = Resources.LoadAll<EnemyStaticData>(AssetPaths.EnemiesStaticData).ToDictionary(x => x.EnemyTypeId, x => x);
      _levelData = Resources.LoadAll<LevelStaticData>(AssetPaths.LevelsStaticData).ToDictionary(x => x.LevelName, x => x);
      _eventData = Resources.LoadAll<EventStaticData>(AssetPaths.EventsStaticData).ToDictionary(x => x.Id, x => x);
      _viewData = Resources.LoadAll<ViewStaticData>(AssetPaths.ViewsStaticData).ToDictionary(x => x.Id, x => x);
    }

    public HeroStaticData GetHeroData() =>
      _heroData;

    public EnemyStaticData GetEnemyData(EnemyTypeId typeId) =>
      _enemyData.TryGetValue(typeId, out EnemyStaticData enemyData) ? enemyData : null;

    public LevelStaticData GetLevelData(string levelName) =>
      _levelData.TryGetValue(levelName, out LevelStaticData levelData) ? levelData : null;

    public ViewStaticData GetViewData(ViewId viewId) =>
      _viewData.TryGetValue(viewId, out ViewStaticData viewData) ? viewData : null;

    public IEnumerable<EventStaticData> GetAllEventsData() {
      foreach (var eventData in _eventData) {
        yield return eventData.Value;
      }
    }
  }
}
