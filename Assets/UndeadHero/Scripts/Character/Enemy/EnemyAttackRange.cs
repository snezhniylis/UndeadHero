using UnityEngine;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(EnemyAttackHero))]
  public class EnemyAttackRange : MonoBehaviour {
    [SerializeField]
    private TriggerObserver _triggerObserver;

    [SerializeField]
    private EnemyAttackHero _attackHeroBehavior;

    private void Awake() {
      _triggerObserver.OnEnteredTrigger += (Collider c) => { EnableAttackHeroBehavior();};
      _triggerObserver.OnExitedTrigger += (Collider c) => { DisableAttackHeroBehavior();};

      DisableAttackHeroBehavior();
    }

    private void EnableAttackHeroBehavior() =>
      _attackHeroBehavior.enabled = true;

    private void DisableAttackHeroBehavior() =>
      _attackHeroBehavior.enabled = false;
  }
}
