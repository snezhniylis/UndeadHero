using System.Collections.Generic;
using UndeadHero.Events;

namespace UndeadHero.Infrastructure.Services.EventFactory {
  public interface IEventFactory : IService {
    IEnumerable<GameEvent> CreateGameEvents();
  }
}
