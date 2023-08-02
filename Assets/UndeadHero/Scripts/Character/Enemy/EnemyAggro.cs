using System.Collections;
using UnityEngine;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(EnemyFollowHero))]
  public class EnemyAggro : MonoBehaviour {
    [SerializeField]
    private TriggerObserver _triggerObserver;
    [SerializeField]
    private EnemyFollowHero _followHeroBehavior;

    [SerializeField]
    private float _cooldown;

    private WaitForSeconds _cooldownSeconds;
    private bool _isFollowingHero;

    private void OnValidate() {
      _triggerObserver = GetComponentInChildren<TriggerObserver>();
      _followHeroBehavior = GetComponent<EnemyFollowHero>();
    }

    private void Awake() {
      _triggerObserver.OnEnteredTrigger += OnEnteredTriggerCallback;
      _triggerObserver.OnExitedTrigger += OnExitedTriggerCallback;

      _cooldownSeconds = new WaitForSeconds(_cooldown);

      DisableFollowHeroBehavior();
    }

    private void OnEnteredTriggerCallback(Collider obj) =>
      StartFollowingHero();

    private void OnExitedTriggerCallback(Collider obj) =>
      StopFollowingHero();

    private void StartFollowingHero() {
      if (!_isFollowingHero) {
        StopAllCoroutines();
        EnableFollowHeroBehavior();

        _isFollowingHero = true;
      }
    }

    private void StopFollowingHero() {
      if (_isFollowingHero) {
        StartCoroutine(StopFollowingHeroRoutine());

        _isFollowingHero = false;
      }
    }

    private IEnumerator StopFollowingHeroRoutine() {
      yield return _cooldownSeconds;
      DisableFollowHeroBehavior();
    }

    private void EnableFollowHeroBehavior() =>
      _followHeroBehavior.enabled = true;

    private void DisableFollowHeroBehavior() =>
      _followHeroBehavior.enabled = false;
  }
}
