using System;
using UndeadHero.Character.Base.Animation;
using UnityEngine;

namespace UndeadHero.Character.Base {
  public abstract class CharacterHealth : MonoBehaviour {
    [SerializeField] private CharacterAnimator _characterAnimator;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    public float Max {
      get => _maxHealth;
      protected set => _maxHealth = value;
    }

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
