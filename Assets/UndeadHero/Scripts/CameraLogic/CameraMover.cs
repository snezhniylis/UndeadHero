using UnityEngine;

namespace UndeadHero.CameraLogic {
  public class CameraMover : MonoBehaviour {
    [SerializeField]
    private float _cameraTilt;
    [SerializeField]
    private float _cameraPan;
    [SerializeField]
    private float _cameraDistance;
    [SerializeField]
    private Vector3 _cameraOffset;

    private Transform _followedTarget;

    public void SetTarget(GameObject target) =>
      _followedTarget = target.transform;

    private void LateUpdate() {
      FollowTarget();
    }

    private void FollowTarget() {
      if (_followedTarget == null) {
        return;
      }

      Quaternion cameraRotation = Quaternion.Euler(_cameraTilt, _cameraPan, 0);
      Vector3 cameraPosition = (cameraRotation * new Vector3(0, 0, -_cameraDistance)) + _followedTarget.position + _cameraOffset;

      transform.SetPositionAndRotation(cameraPosition, cameraRotation);
    }
  }
}
