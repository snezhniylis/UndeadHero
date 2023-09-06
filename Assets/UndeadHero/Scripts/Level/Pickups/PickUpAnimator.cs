using System;
using UnityEngine;

namespace UndeadHero.Level.Pickups {
  public class PickUpAnimator : MonoBehaviour {
    private const int BobbingSpeed = 2;
    private const float BobbingStrength = 0.3f;
    private const int RotationSpeed = 80;

    [SerializeField] private Transform _pickupObject;

    private Vector3 _initialPosition;

    private void Awake() =>
      _initialPosition = _pickupObject.localPosition;

    private void Update() {
      OscillatePickupObject();
      RotatePickupObject();
    }

    private void OscillatePickupObject() =>
      _pickupObject.localPosition = _initialPosition + CalculateYOffset();

    private void RotatePickupObject() =>
      _pickupObject.Rotate(CalculateRotation());

    private static Vector3 CalculateYOffset() =>
      Vector3.up * (MathF.Sin(Time.time * BobbingSpeed) * BobbingStrength);

    private static Vector3 CalculateRotation() =>
      Vector3.up * (RotationSpeed * Time.deltaTime);
  }
}
