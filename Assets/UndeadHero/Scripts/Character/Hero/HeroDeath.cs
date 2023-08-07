using UnityEngine;

namespace UndeadHero.Character.Hero {
  [RequireComponent(typeof(HeroHealth), typeof(HeroMover), typeof(HeroAnimator))]
  public class HeroDeath : MonoBehaviour {
    [SerializeField]
    private HeroHealth _heroHealth;
    [SerializeField]
    private HeroMover _heroMover;
    [SerializeField]
    private HeroAnimator _heroAnimator;

    [SerializeField]
    private GameObject _deathFx;

    private void Start() {
      _heroHealth.OnHealthChanged += OnHealthChangedCallback;
    }

    private void OnHealthChangedCallback() {
      if (_heroHealth.IsOutOfHp()) {
        Die();
      }
    }

    private void Die() {
      _heroMover.enabled = false;
      _heroAnimator.Die();

      Instantiate(_deathFx, transform.position, Quaternion.identity);
    }
  }
}
