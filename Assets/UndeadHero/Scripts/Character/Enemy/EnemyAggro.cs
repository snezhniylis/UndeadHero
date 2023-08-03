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
    private float _interestLossSpan;

    private WaitForSeconds _interestLossSeconds;
    private bool _isFollowingHero;

    private void OnValidate() {
      _followHeroBehavior = GetComponent<EnemyFollowHero>();
    }

    private void Awake() {
      _triggerObserver.OnEnteredTrigger += (Collider c) => { StartFollowingHero(); };
      _triggerObserver.OnExitedTrigger += (Collider c) => { StopFollowingHero(); };

      _interestLossSeconds = new WaitForSeconds(_interestLossSpan);

      DisableFollowHeroBehavior();
    }

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
      yield return _interestLossSeconds;
      DisableFollowHeroBehavior();
    }

    private void EnableFollowHeroBehavior() =>
      _followHeroBehavior.enabled = true;

    private void DisableFollowHeroBehavior() =>
      _followHeroBehavior.enabled = false;
  }
}
