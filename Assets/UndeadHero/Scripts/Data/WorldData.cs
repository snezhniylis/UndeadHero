using System;
using System.Collections.Generic;

namespace UndeadHero.Data {
  [Serializable]
  public class WorldData {
    public string Level;
    public Vector3Data PlayerPosition;
    public List<string> DefeatedSpawners = new();
  }
}
