using UndeadHero.Character.Base.Animation;
using UnityEngine;

namespace UndeadHero.Character.Base {
  public abstract class CharacterAttack : MonoBehaviour {
    private const string HitTargetAnimationEvent = "HitTarget";

    [SerializeField] private CharacterAnimator _characterAnimator;

    [SerializeField] private float _attackCooldown;
    [SerializeField] private Vector3 _attackImpactOrigin;
    [SerializeField] private float _attackImpactRadius;
    [SerializeField] private float _damage;

    protected int HitCollisionMask;
    protected Collider[] HitCollidersBuffer;

    protected abstract void InitializeCollisionParameters();
    protected abstract Vector3 GetAttackTarget();
    protected abstract bool ShouldAttack();

    private float _cooldownExpirationTime;

    protected virtual void Awake() =>
      InitializeCollisionParameters();

    private void Update() {
      if (ShouldAttack() && CanAttack()) {
        InitiateAttack();
      }
    }

    private void OnDrawGizmosSelected() =>
      CharacterDebug.DrawImpactSphere(LocalToWorld(_attackImpactOrigin), _attackImpactRadius);

    private void OnAnimationEvent(string eventName) {
      if (eventName == HitTargetAnimationEvent) {
        ProcessHit();
      }
    }

    private void ProcessHit() {
      int targetsHitCount = PerformHitDetection();
      if (targetsHitCount > 0) {
        for (var i = 0; i < targetsHitCount; i++) {
          HitCollidersBuffer[i].transform.GetComponent<CharacterHealth>().TakeDamage(_damage);
        }

        CharacterDebug.DrawRaysTimed(LocalToWorld(_attackImpactOrigin), _attackImpactRadius, 0.5f);
      }
    }

    private void InitiateAttack() {
      transform.LookAt(GetAttackTarget());
      _characterAnimator.Attack();
      ActivateCooldown();
    }

    private int PerformHitDetection() =>
      Physics.OverlapSphereNonAlloc(LocalToWorld(_attackImpactOrigin), _attackImpactRadius, HitCollidersBuffer, HitCollisionMask);

    private Vector3 LocalToWorld(Vector3 localPosition) =>
      transform.TransformPoint(localPosition);

    private bool CanAttack() =>
      !(IsCooldownActive() || _characterAnimator.State == AnimatorState.Attacking);

    private void ActivateCooldown() =>
      _cooldownExpirationTime = Time.time + _attackCooldown;

    private bool IsCooldownActive() =>
      _cooldownExpirationTime > Time.time;
  }
}
