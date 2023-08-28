using System;

namespace UndeadHero.Data {
  [Serializable]
  public class PlayerProgress {
    public CurrentLevelData CurrentLevel;
    public PlayerStatsData PlayerStats = new();
    public EventStatsData EventStats = new();
  }
}
