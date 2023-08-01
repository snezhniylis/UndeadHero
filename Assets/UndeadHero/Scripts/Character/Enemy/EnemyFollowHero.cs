using UndeadHero.Infrastructure.Factory;
using UndeadHero.Infrastructure.Services;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(NavMeshAgent))]
  public class EnemyFollowHero : MonoBehaviour {
    private const float MinimumDistance = 1.5f;

    [SerializeField]
    private NavMeshAgent _agent;

    private IGameFactory _factory;
    private Transform _heroTransform;

    private void OnValidate() {
      _agent = GetComponent<NavMeshAgent>();
    }

    private void Start() {
      _factory = GameServices.Container.Single<IGameFactory>();
      if (_factory.HeroGameObject == null) {
        _factory.OnHeroCreated += CacheHeroTransform;
      }
      else {
        CacheHeroTransform();
      }
    }

    private void Update() {
      FollowHero();
    }

    private void FollowHero() {
      if (IsHeroTransformCached() && CanComeCloser()) {
        _agent.destination = _heroTransform.position;
      }
    }

    private bool IsHeroTransformCached() =>
      _heroTransform != null;

    private bool CanComeCloser() =>
      Vector3.Distance(transform.position, _heroTransform.position) >= MinimumDistance;

    private void CacheHeroTransform() =>
      _heroTransform = _factory.HeroGameObject.transform;
  }
}
