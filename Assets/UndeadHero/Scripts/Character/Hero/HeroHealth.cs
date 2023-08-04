using System;
using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace UndeadHero.Character.Hero {
  [RequireComponent(typeof(HeroAnimator))]
  public class HeroHealth : MonoBehaviour, IPersistentProgressWriter {
    [SerializeField]
    private HeroAnimator _animator;

    private float _currentHealth;
    public float Max { get; private set; }
    public float Current {
      get => _currentHealth;
      private set {
        if (_currentHealth != value) {
          _currentHealth = value;
          OnHealthChanged?.Invoke();
        }
      }
    }

    public Action OnHealthChanged;

    public void LoadProgress(PlayerProgress progress) {
      Max = progress.HeroData.MaxHp;
      Current = progress.HeroData.CurrentHp;
    }

    public void UpdateProgress(PlayerProgress progress) {
      progress.HeroData.MaxHp = Max;
      progress.HeroData.CurrentHp = Current;
    }

    public void TakeDamage(float damage) {
      if (IsOutOfHp())
        return;

      Current = Mathf.Max(Current - damage, 0);
      _animator.GetHit();
    }

    public bool IsOutOfHp() =>
      Current == 0;
  }
}
