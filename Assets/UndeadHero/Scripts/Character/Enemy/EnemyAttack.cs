using UndeadHero.Character.Base;
using UndeadHero.Infrastructure.Services;
using UndeadHero.Infrastructure.Services.Factory;
using UnityEngine;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(EnemyAnimator))]
  public class EnemyAttack : CharacterAttack {
    private const string TargetCollisionLayerName = "Hero";
    private const int MaxTargetsHitAtOnce = 1;

    [SerializeField] private TriggerObserver _attackRangeTrigger;

    private Transform _heroTransform;
    private bool _isHeroClose;

    protected override void Awake() {
      base.Awake();

      _attackRangeTrigger.OnEnteredTrigger += (_) => _isHeroClose = true;
      _attackRangeTrigger.OnExitedTrigger += (_) => _isHeroClose = false;

      var factory = GameServices.Container.Single<IGameFactory>();
      factory.OnHeroCreated += () => { _heroTransform = factory.HeroGameObject.transform; };
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
