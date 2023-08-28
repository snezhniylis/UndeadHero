using System;
using System.Collections.Generic;

namespace UndeadHero.Data {
  [Serializable]
  public class CurrentLevelData {
    public string Name;
    public Vector3Data PlayerPosition;
    public float PlayerHp;
    public List<string> DefeatedSpawners = new();
  }
}
