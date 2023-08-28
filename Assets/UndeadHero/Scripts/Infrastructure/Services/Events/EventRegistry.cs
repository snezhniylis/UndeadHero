using System.Collections.Generic;
using UndeadHero.Data;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.StaticDataManagement;
using UndeadHero.StaticData.Events;

namespace UndeadHero.Infrastructure.Services.Events {
  public class EventRegistry : IEventRegistry {
    private readonly IStaticDataProvider _staticDataProvider;

    private readonly Dictionary<EventId, GameEvent> _events = new();

    public EventRegistry(IStaticDataProvider staticDataProvider) {
      _staticDataProvider = staticDataProvider;
    }

    public IEnumerable<GameEvent> GetAllEvents() =>
      _events.Values;

    public GameEvent GetEvent(EventId eventId) =>
      _events[eventId];

    public void LoadGameEvents(PlayerProgress progress) {
      foreach (EventStaticData eventData in _staticDataProvider.GetAllEventsData()) {
        GameEvent gameEvent = new(
          eventData.Id,
          eventData.Conditions,
          eventData.Icon,
          eventData.ViewId,
          progress
        );

        _events[eventData.Id] = gameEvent;
      }
    }
  }
}
