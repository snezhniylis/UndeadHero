using UnityEngine;
using UnityEngine.SceneManagement;
using UndeadHero.Data;
using UndeadHero.Infrastructure.Services;
using UndeadHero.Infrastructure.Services.Input;
using UndeadHero.Infrastructure.Services.PersistentProgress;

namespace UndeadHero.Character {
  [RequireComponent(typeof(CharacterController))]
  public class CharacterMover : MonoBehaviour, IPersistentProgressWriter {
    private static readonly Vector3 PrecautionaryWarpHeightOffset = new(0, 0.5f, 0);

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
      _inputService = GameServices.Container.Single<IInputService>();
      _camera = Camera.main;
    }

    private void Update() {
      UpdateMovement();
    }

    private void UpdateMovement() {
      Vector3 inputAxis = _inputService.MovementAxis;
      float inputMagnitude = inputAxis.magnitude;
      bool isThereAnyInput = inputMagnitude > Mathf.Epsilon;

      Vector3 movementVector;
      if (isThereAnyInput) {
        Vector3 direction = CalculateHeroDirection(inputAxis);
        transform.forward = direction;

        movementVector = direction * CalculateHeroSpeed(inputMagnitude);
      }
      else {
        movementVector = Vector3.zero;
      }

      movementVector += Physics.gravity;
      _characterController.Move(movementVector * Time.deltaTime);
    }

    private Vector3 CalculateHeroDirection(Vector3 controlsAxis) {
      Vector3 direction = _camera.transform.TransformDirection(controlsAxis);
      direction.y = 0;
      return direction.normalized;
    }

    private float CalculateHeroSpeed(float controlsStrength) {
      float speedFactor = Mathf.Min(controlsStrength, 1);
      speedFactor = EaseOutCubic(speedFactor);
      return speedFactor * _movementSpeed;
    }

    private static float EaseOutCubic(float value) {
      value--;
      return (value * value * value + 1);
    }

    public void UpdateProgress(PlayerProgress progress) {
      progress.WorldData.Level = GetCurrentLevelName();
      progress.WorldData.PlayerPosition = transform.position.AsVectorData();
    }

    public void LoadProgress(PlayerProgress progress) {
      if (GetCurrentLevelName() == progress.WorldData.Level) {
        Vector3Data savedPosition = progress.WorldData.PlayerPosition;
        if (savedPosition != null) {
          Warp(savedPosition.AsUnityVector());
        }
      }
    }

    private void Warp(Vector3 position) {
      _characterController.enabled = false;
      transform.position = position + PrecautionaryWarpHeightOffset;
      _characterController.enabled = true;
    }

    private static string GetCurrentLevelName() =>
      SceneManager.GetActiveScene().name;
  }
}
