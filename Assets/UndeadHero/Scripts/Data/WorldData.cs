using System;

namespace UndeadHero.Data {
  [Serializable]
  public class WorldData {
    public string Level;
    public Vector3Data PlayerPosition;

    public WorldData(string level) {
      Level = level;
    }
  }
}
