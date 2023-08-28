using System;
using UndeadHero.Data;
using UndeadHero.StaticData.Events;
using UndeadHero.UI.Views;
using UnityEngine;

namespace UndeadHero.Events {
  public class GameEvent {
    public readonly EventId Id;
    public readonly Sprite Icon;
    public readonly ViewId ViewId;

    private readonly EventConditionBase[] _conditions;

    private readonly PlayerProgress _playerProgress;

    private bool _isFinished;

    public Action OnEventCompleted;

    public GameEvent(EventId id, EventConditionBase[] conditions, Sprite icon, ViewId viewId, PlayerProgress playerProgress) {
      Id = id;
      Icon = icon;
      ViewId = viewId;
      _conditions = conditions;

      _playerProgress = playerProgress;
      RetrieveStatusFromPlayerProgress();
    }

    public void MarkFinished() {
      _isFinished = true;

      OnEventCompleted?.Invoke();

      SaveStatusToPlayerProgress();
    }

    public bool IsActive() =>
      !_isFinished && AreEventConditionsMet();

    private bool AreEventConditionsMet() {
      foreach (EventConditionBase eventCondition in _conditions) {
        if (!eventCondition.IsTrue()) {
          return false;
        }
      }

      return true;
    }

    private void RetrieveStatusFromPlayerProgress() {
      _isFinished = _playerProgress.EventStats.FinishedEvents.Contains(Id);
    }

    private void SaveStatusToPlayerProgress() {
      if (_isFinished && !_playerProgress.EventStats.FinishedEvents.Contains(Id)) {
        _playerProgress.EventStats.FinishedEvents.Add(Id);
      }
    }
  }
}
