using UnityEngine;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(EnemyAttack))]
  public class EnemyAttackRange : MonoBehaviour {
    [SerializeField]
    private TriggerObserver _triggerObserver;

    [SerializeField]
    private EnemyAttack _attackBehavior;

    private void Awake() {
      _triggerObserver.OnEnteredTrigger += (Collider c) => { EnableAttackBehavior(); };
      _triggerObserver.OnExitedTrigger += (Collider c) => { DisableAttackBehavior(); };

      DisableAttackBehavior();
    }

    private void EnableAttackBehavior() =>
      _attackBehavior.enabled = true;

    private void DisableAttackBehavior() =>
      _attackBehavior.enabled = false;
  }
}
