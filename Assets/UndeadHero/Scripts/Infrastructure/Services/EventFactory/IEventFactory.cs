using System.Collections.Generic;
using UndeadHero.Events;

namespace UndeadHero.Infrastructure.Services.EventFactory {
  public interface IEventFactory {
    IEnumerable<GameEvent> CreateGameEvents();
  }
}
