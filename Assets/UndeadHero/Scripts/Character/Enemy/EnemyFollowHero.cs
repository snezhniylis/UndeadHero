using UnityEngine;
using UnityEngine.AI;

namespace UndeadHero.Character.Enemy {
  [RequireComponent(typeof(NavMeshAgent))]
  public class EnemyFollowHero : MonoBehaviour {
    private const float MinimumDistance = 1.5f;

    [SerializeField] private NavMeshAgent _agent;

    private Transform _heroTransform;

    public void Initialize(Transform heroTransform) {
      _heroTransform = heroTransform;
    }

    private void Update() =>
      FollowHero();

    private void FollowHero() {
      if (CanComeCloser()) {
        _agent.destination = _heroTransform.position;
      }
    }

    private bool CanComeCloser() =>
      Vector3.Distance(transform.position, _heroTransform.position) >= MinimumDistance;
  }
}
