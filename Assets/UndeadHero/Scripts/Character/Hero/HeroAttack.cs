using UndeadHero.Character.Base;
using UndeadHero.Infrastructure.Services.Input;
using UnityEngine;

namespace UndeadHero.Character.Hero {
  [RequireComponent(typeof(HeroAnimator))]
  public class HeroAttack : CharacterAttack {
    private const string TargetCollisionLayerName = "Enemy";
    private const int MaxTargetsHitAtOnce = 3;

    private IInputService _inputService;

    public void Initialize(float damage, float cooldown, Vector3 impactOrigin, float impactRadius, IInputService inputService) {
      base.Initialize(damage, cooldown, impactOrigin, impactRadius);
      _inputService = inputService;
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
