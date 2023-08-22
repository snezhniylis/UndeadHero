using System;
using UnityEngine;

namespace UndeadHero.StaticData.Events {
  [CreateAssetMenu(fileName = "DateRange", menuName = "StaticData/Event Conditions/DateRange")]
  public class EventConditionDateRange : EventConditionBase {
    public string From;
    public string To;

    public override bool IsTrue() =>
      StringToDateTime(From) < DateTime.Now && DateTime.Now < StringToDateTime(To);

    private static DateTime StringToDateTime(string input) =>
      DateTime.Parse(input);
  }
}
