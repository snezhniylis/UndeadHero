using System;

namespace UndeadHero.Data {
  [Serializable]
  public class PlayerProgress {
    public WorldData WorldData;
    public HeroData HeroData;

    public PlayerProgress(string initialLevel) {
      WorldData = new WorldData(initialLevel);
      HeroData = new HeroData();
    }
  }
}
