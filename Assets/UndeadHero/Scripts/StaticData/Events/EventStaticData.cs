using UndeadHero.UI.Views;
using UnityEngine;

namespace UndeadHero.StaticData.Events {
  [CreateAssetMenu(fileName = "EventData", menuName = "StaticData/Event")]
  public class EventStaticData : ScriptableObject {
    public string Id;
    public EventConditionBase[] Conditions;

    [Header("UI")] public Sprite Icon;
    public ViewId ViewId;
  }
}
