using System;

namespace UndeadHero.Data {
  [Serializable]
  public class PlayerProgress {
    public WorldData WorldData = new();
    public HeroData HeroData = new();
    public EventData EventData = new();
  }
}
