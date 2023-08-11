using UndeadHero.Character.Base.Animation;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(NavMeshAgent))]
  public class EnemyAnimator : CharacterAnimator {
    [SerializeField] private NavMeshAgent _navMeshAgent;

    private void Update() {
      float enemySpeed = _navMeshAgent.velocity.magnitude;
      if (enemySpeed > 0.01) {
        Move(enemySpeed);
      }
      else {
        StopMoving();
      }
    }
  }
}
