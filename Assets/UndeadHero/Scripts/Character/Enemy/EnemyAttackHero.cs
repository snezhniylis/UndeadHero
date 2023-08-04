using System.Linq;
using UndeadHero.Character.Hero;
using UndeadHero.Infrastructure.Factory;
using UndeadHero.Infrastructure.Services;
using UnityEngine;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(EnemyAnimator))]
  public class EnemyAttackHero : MonoBehaviour {
    private const string HeroCollisionLayerName = "Hero";
    private const string HitTargetAnimationEvent = "HitTarget";
    private const string FinishedAttackAnimationEvent = "FinishedAttack";

    [SerializeField]
    private EnemyAnimator _animator;

    [SerializeField]
    private float _attackCooldown;
    [SerializeField]
    private Vector3 _attackImpactOrigin;
    [SerializeField]
    private float _attackImpactRadius = 0.5f;
    [SerializeField]
    private float _damage;

    private IGameFactory _factory;

    private Transform _heroTransform;
    private bool _isAttackAnimationPlaying;
    private float _cooldownExpirationTime;

    private readonly Collider[] _hitCollisionBuffer = new Collider[1];
    private int _hitCollisionMask;

    private void OnValidate() {
      _animator = GetComponent<EnemyAnimator>();
    }

    private void Awake() {
      _factory = GameServices.Container.Single<IGameFactory>();
      _factory.OnHeroCreated += () => {
        _heroTransform = _factory.HeroGameObject.transform;
      };

      _hitCollisionMask = LayerMask.GetMask(HeroCollisionLayerName);
    }

    private void Update() {
      if (CanAttack()) {
        InitiateAttack();
      }
    }

    private void OnDrawGizmosSelected() {
      Vector3 attackImpactOrigin = LocalToWorld(_attackImpactOrigin);
      Gizmos.color = new Color32(20, 255, 30, 170);
      Gizmos.DrawSphere(attackImpactOrigin, 0.1f);
      Gizmos.color = new Color32(20, 255, 30, 80);
      Gizmos.DrawSphere(attackImpactOrigin, _attackImpactRadius);
    }

    private void OnAnimationEvent(string eventName) {
      if (eventName == HitTargetAnimationEvent) {
        ProcessHit();
      }
      if (eventName == FinishedAttackAnimationEvent) {
        _isAttackAnimationPlaying = false;
      }
    }

    private void ProcessHit() {
      if (Hit(out Collider heroCollider)) {
        heroCollider.transform.GetComponent<HeroHealth>().TakeDamage(_damage);

        CharacterDebug.DrawRaysTimed(LocalToWorld(_attackImpactOrigin), _attackImpactRadius, 0.5f);
      }
    }

    private bool Hit(out Collider heroCollider) {
      int hitsAmount = Physics.OverlapSphereNonAlloc(LocalToWorld(_attackImpactOrigin), _attackImpactRadius, _hitCollisionBuffer, _hitCollisionMask);
      heroCollider = _hitCollisionBuffer.FirstOrDefault();
      return hitsAmount > 0;
    }

    private void InitiateAttack() {
      transform.LookAt(_heroTransform);
      _animator.Attack();
      _isAttackAnimationPlaying = true;
      ActivateCooldown();
    }

    private Vector3 LocalToWorld(Vector3 localPosition) =>
      transform.TransformPoint(localPosition);

    private bool CanAttack() =>
      !(IsCooldownActive() || _isAttackAnimationPlaying);

    private void ActivateCooldown() =>
      _cooldownExpirationTime = Time.time + _attackCooldown;

    private bool IsCooldownActive() =>
      _cooldownExpirationTime > Time.time;
  }
}
