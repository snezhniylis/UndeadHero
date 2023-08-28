using System.Collections.Generic;
using UndeadHero.Data;
using UndeadHero.Events;
using UndeadHero.StaticData.Events;

namespace UndeadHero.Infrastructure.Services.Events {
  public interface IEventRegistry {
    IEnumerable<GameEvent> GetAllEvents();
    GameEvent GetEvent(EventId eventId);
    void LoadGameEvents(PlayerProgress progress);
  }
}
