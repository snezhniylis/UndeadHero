using UnityEngine;

namespace UndeadHero.StaticData.Events {
  public abstract class EventConditionBase : ScriptableObject {
    public abstract bool IsTrue();
  }
}
