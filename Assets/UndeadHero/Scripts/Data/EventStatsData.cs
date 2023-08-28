using System;
using System.Collections.Generic;
using UndeadHero.StaticData.Events;

namespace UndeadHero.Data {
  [Serializable]
  public class EventStatsData {
    public List<EventId> FinishedEvents = new();
  }
}
