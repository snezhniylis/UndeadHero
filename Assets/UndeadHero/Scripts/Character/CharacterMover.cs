using UnityEngine;
using UndeadHero.CameraLogic;
using UndeadHero.Services.Input;
using UndeadHero.Infrastructure;

namespace UndeadHero.Character {
  [RequireComponent(typeof(CharacterController))]
  public class CharacterMover : MonoBehaviour {
    [SerializeField]
    private CharacterController _characterController;
    [SerializeField]
    private float _movementSpeed = 4.0f;

    private IInputService _inputService;
    private Camera _camera;

    private void OnValidate() {
      _characterController = GetComponent<CharacterController>();
    }

    private void Awake() {
      _inputService = Game.InputService;
      _camera = Camera.main;

      SelfAssignToCamera();
    }

    private void Update() {
      Vector3 movementVector = _inputService.MovementAxis;
      if (movementVector.sqrMagnitude > Mathf.Epsilon) {
        movementVector = _camera.transform.TransformDirection(movementVector);
        movementVector.y = 0;
        movementVector.Normalize();

        transform.forward = movementVector;
      }
      else {
        movementVector = Vector3.zero;
      }

      movementVector += Physics.gravity;
      _characterController.Move(_movementSpeed * Time.deltaTime * movementVector);
    }

    private void SelfAssignToCamera() {
      if (_camera.TryGetComponent<CameraMover>(out var cameraMover)) {
        cameraMover.SetTarget(transform);
      }
    }
  }
}