using UndeadHero.Infrastructure.Factory;
using UndeadHero.Infrastructure.Services;
using UnityEngine;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(EnemyAnimator))]
  public class EnemyAttack : CharacterAttack {
    private const string TargetCollisionLayerName = "Hero";
    private const int MaxTargetsHitAtOnce = 1;

    [SerializeField]
    private TriggerObserver _attackRangeTrigger;

    private Transform _heroTransform;
    private bool _isHeroClose;

    protected override void Awake() {
      base.Awake();

      _attackRangeTrigger.OnEnteredTrigger += (Collider c) => _isHeroClose = true;
      _attackRangeTrigger.OnExitedTrigger += (Collider c) => _isHeroClose = false;

      var factory = GameServices.Container.Single<IGameFactory>();
      factory.OnHeroCreated += () => {
        _heroTransform = factory.HeroGameObject.transform;
      };
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
