using UndeadHero.Infrastructure.States;
using UnityEngine;
using VContainer;

namespace UndeadHero.Level {
  [RequireComponent(typeof(BoxCollider))]
  public class LoadLevelTrigger : MonoBehaviour {
    [SerializeField] private string _levelToLoad;
    [SerializeField] private BoxCollider _collider;

    private GameStateMachine _stateMachine;

    [Inject]
    private void Construct(GameStateMachine stateMachine) {
      _stateMachine = stateMachine;
    }

    private void OnTriggerEnter(Collider other) {
      _stateMachine.Enter<StateLoadLevel, string>(_levelToLoad);
      gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
      Gizmos.color = new Color32(100, 200, 250, 130);
      Gizmos.matrix = transform.localToWorldMatrix;
      Gizmos.DrawCube(_collider.center, _collider.size);
      Gizmos.matrix = Matrix4x4.identity;
    }
  }
}
