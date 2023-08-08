using UndeadHero.Infrastructure.Factory;
using UndeadHero.Infrastructure.Services;
using UnityEngine;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(EnemyAnimator))]
  public class EnemyAttack : CharacterAttack {
    private const string TargetCollisionLayerName = "Hero";
    private const int MaxTargetsHitAtOnce = 1;

    private IGameFactory _factory;
    private Transform _heroTransform;

    protected override void Awake() {
      base.Awake();

      _factory = GameServices.Container.Single<IGameFactory>();
      _factory.OnHeroCreated += () => {
        _heroTransform = _factory.HeroGameObject.transform;
      };
    }

    protected override void InitializeCollisionParameters() {
      HitCollisionMask = LayerMask.GetMask(TargetCollisionLayerName);
      HitCollidersBuffer = new Collider[MaxTargetsHitAtOnce];
    }

    protected override bool ShouldAttack() =>
      true;

    protected override Vector3 GetAttackTarget() =>
      _heroTransform.position;
  }
}
