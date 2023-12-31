using System;
using UndeadHero.Character.Base.Animation;
using UnityEngine;

namespace UndeadHero.Character.Base {
  [RequireComponent(typeof(CharacterHealth), typeof(CharacterAnimator))]
  public abstract class CharacterDeath : MonoBehaviour {
    [SerializeField] private CharacterHealth _characterHealth;
    [SerializeField] private CharacterAnimator _characterAnimator;

    [SerializeField] private GameObject _deathFx;

    public Action OnDied;

    private void OnEnable() =>
      _characterHealth.OnHealthChanged += OnHealthChanged;

    private void OnDisable() =>
      _characterHealth.OnHealthChanged -= OnHealthChanged;

    protected abstract void ApplyDeathEffect();

    private void OnHealthChanged() {
      if (_characterHealth.IsOutOfHp()) {
        Die();
      }
    }

    private void Die() {
      _characterAnimator.Die();
      SpawnDeathFx();
      ApplyDeathEffect();
      OnDied?.Invoke();
    }

    private void SpawnDeathFx() =>
      Instantiate(_deathFx, transform.position, Quaternion.identity);
  }
}
