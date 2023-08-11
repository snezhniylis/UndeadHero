using UndeadHero.Character.Base;
using UndeadHero.Infrastructure.Services;
using UndeadHero.Infrastructure.Services.Input;
using UnityEngine;

namespace UndeadHero.Character.Hero {
  [RequireComponent(typeof(HeroAnimator))]
  public class HeroAttack : CharacterAttack {
    private const string TargetCollisionLayerName = "Enemy";
    private const int MaxTargetsHitAtOnce = 3;

    private IInputService _inputService;

    protected override void Awake() {
      base.Awake();

      _inputService = GameServices.Container.Single<IInputService>();
    }

    protected override void InitializeCollisionParameters() {
      HitCollisionMask = LayerMask.GetMask(TargetCollisionLayerName);
      HitCollidersBuffer = new Collider[MaxTargetsHitAtOnce];
    }

    protected override bool ShouldAttack() =>
      _inputService.AttackTriggered;

    protected override Vector3 GetAttackTarget() =>
      transform.position + transform.forward;
  }
}
