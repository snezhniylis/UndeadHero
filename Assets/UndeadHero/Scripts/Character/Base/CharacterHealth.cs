using System;
using UndeadHero.Character.Base.Animation;
using UnityEngine;

namespace UndeadHero.Character.Base {
  public abstract class CharacterHealth : MonoBehaviour {
    [SerializeField] private CharacterAnimator _characterAnimator;

    private float _currentHealth;

    public float Max { get; protected set; }

    public float Current {
      get => _currentHealth;
      protected set {
        if (_currentHealth != value) {
          _currentHealth = value;
          OnHealthChanged?.Invoke();
        }
      }
    }

    public Action OnHealthChanged;

    public void Initialize(float maxHealth, float currentHealth) {
      Max = maxHealth;
      Current = currentHealth;
    }

    public void TakeDamage(float damage) {
      if (IsOutOfHp())
        return;

      Current = Mathf.Max(Current - damage, 0);
      _characterAnimator.GetHit();
    }

    public bool IsOutOfHp() =>
      Current == 0;
  }
}
