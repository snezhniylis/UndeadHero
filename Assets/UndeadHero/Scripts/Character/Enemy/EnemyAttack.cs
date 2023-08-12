using UndeadHero.Character.Base;
using UnityEngine;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(EnemyAnimator))]
  public class EnemyAttack : CharacterAttack {
    private const string TargetCollisionLayerName = "Hero";
    private const int MaxTargetsHitAtOnce = 1;

    [SerializeField] private TriggerObserver _attackRangeTrigger;

    private Transform _heroTransform;
    private bool _isHeroClose;

    public void Initialize(Transform heroTransform, float damage, float cooldown, Vector3 impactOrigin, float impactRadius) {
      _heroTransform = heroTransform;
      base.Initialize(damage, cooldown, impactOrigin, impactRadius);
    }

    protected override void Awake() {
      base.Awake();

      _attackRangeTrigger.OnEnteredTrigger += (_) => _isHeroClose = true;
      _attackRangeTrigger.OnExitedTrigger += (_) => _isHeroClose = false;
    }

    protected override void InitializeCollisionParameters() {
      HitCollisionMask = LayerMask.GetMask(TargetCollisionLayerName);
      HitCollidersBuffer = new Collider[MaxTargetsHitAtOnce];
    }

    protected override bool ShouldAttack() =>
      _isHeroClose;

    protected override Vector3 GetAttackTarget() =>
      _heroTransform.position;
  }
}
