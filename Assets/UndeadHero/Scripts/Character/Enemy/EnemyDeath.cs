using System.Collections;
using UnityEngine;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(EnemyFollowHero))]
  public class EnemyDeath : CharacterDeath {
    private const float SelfDestroySeconds = 3f;

    [SerializeField]
    private EnemyFollowHero _followHeroBehavior;

    protected override void ApplyDeathEffect() {
      _followHeroBehavior.enabled = false;

      StartCoroutine(SelfDestroyRoutine());
    }

    private IEnumerator SelfDestroyRoutine() {
      yield return new WaitForSeconds(SelfDestroySeconds);
      Destroy(gameObject);
    }
  }
}
