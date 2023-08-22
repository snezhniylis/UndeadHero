using System;
using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.StaticData.Events;
using UndeadHero.UI.Views;
using UnityEngine;

namespace UndeadHero.Events {
  public class GameEvent : IPersistentProgressWriter {
    public readonly string Id;
    public readonly Sprite Icon;
    public readonly ViewId ViewId;

    private readonly EventConditionBase[] _conditions;

    private bool _isFinished;

    public Action OnEventCompleted;

    public GameEvent(string id, EventConditionBase[] conditions, Sprite icon, ViewId viewId) {
      Id = id;
      Icon = icon;
      ViewId = viewId;

      _conditions = conditions;
    }

    public void ReadProgress(PlayerProgress progress) {
      if (progress != null && progress.EventData.FinishedEvents.Contains(Id)) {
        MarkFinished();
      }
    }

    public void WriteProgress(PlayerProgress progress) {
      if (_isFinished && !progress.EventData.FinishedEvents.Contains(Id)) {
        progress.EventData.FinishedEvents.Add(Id);
      }
    }

    public void MarkFinished() {
      _isFinished = true;
      OnEventCompleted?.Invoke();
    }


    public bool IsActive() =>
      !_isFinished || AreEventConditionsMet();

    public bool AreEventConditionsMet() {
      foreach (EventConditionBase eventCondition in _conditions) {
        if (!eventCondition.IsTrue()) {
          return false;
        }
      }

      return true;
    }
  }
}
