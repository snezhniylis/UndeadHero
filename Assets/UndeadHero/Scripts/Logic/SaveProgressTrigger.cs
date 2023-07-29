using UndeadHero.Infrastructure.Services;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace UndeadHero.Logic {
  [RequireComponent(typeof(BoxCollider))]
  public class SaveProgressTrigger : MonoBehaviour {
    private IPersistentProgressService _progressService;

    [SerializeField] private BoxCollider _collider;

    private void OnValidate() {
      _collider = GetComponent<BoxCollider>();
    }

    private void Awake() {
      _progressService = GameServices.Container.Single<IPersistentProgressService>();
    }

    private void OnTriggerEnter(Collider other) {
      _progressService.SaveProgress();
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
