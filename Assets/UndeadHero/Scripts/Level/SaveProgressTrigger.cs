using UndeadHero.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using VContainer;

namespace UndeadHero.Level {
  [RequireComponent(typeof(BoxCollider))]
  public class SaveProgressTrigger : MonoBehaviour {
    private IPersistentProgressService _progressService;

    [SerializeField] private BoxCollider _collider;

    [Inject]
    private void Construct(IPersistentProgressService progressService) {
      _progressService = progressService;
    }

    private void OnTriggerEnter(Collider other) {
      _progressService.SaveLevelProgress();
      Debug.Log("Progress Saved");
      gameObject.SetActive(false);
    }

    private void OnDrawGizmos() {
      Gizmos.color = new Color32(30, 200, 30, 130);
      Gizmos.matrix = transform.localToWorldMatrix;
      Gizmos.DrawCube(_collider.center, _collider.size);
      Gizmos.matrix = Matrix4x4.identity;
    }
  }
}
