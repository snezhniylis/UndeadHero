using System.Collections.Generic;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.Infrastructure.Services.StaticDataManagement;
using UndeadHero.StaticData.Events;

namespace UndeadHero.Infrastructure.Services.EventFactory {
  public class EventFactory : IEventFactory {
    private readonly IStaticDataProvider _staticDataProvider;
    private readonly IPersistentProgressService _persistentProgress;

    public EventFactory(IStaticDataProvider staticDataProvider, IPersistentProgressService persistentProgress) {
      _staticDataProvider = staticDataProvider;
      _persistentProgress = persistentProgress;
    }

    public IEnumerable<GameEvent> CreateGameEvents() {
      foreach (EventStaticData eventData in _staticDataProvider.GetAllEventsData()) {
        GameEvent gameEvent = new(
          eventData.Id,
          eventData.Conditions,
          eventData.Icon,
          eventData.ViewId
        );

        _persistentProgress.BindObject(gameEvent);

        yield return gameEvent;
      }
    }
  }
}
