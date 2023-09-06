using System;
using System.Collections;
using UnityEngine;

namespace UndeadHero.Level.Pickups {
  public class PickUpTrigger : MonoBehaviour {
    private const float SelfDestroySeconds = 1f;

    [SerializeField] private GameObject _pickupObject;

    private bool _wasPickedUp;

    public Action<Collider> OnHeroPickUp;

    private void OnTriggerEnter(Collider heroCollider) {
      if (CanBePickedUp()) {
        PerformPickup(heroCollider);
      }
    }

    private bool CanBePickedUp() =>
      !_wasPickedUp;

    private void PerformPickup(Collider heroCollider) {
      OnHeroPickUp?.Invoke(heroCollider);

      _wasPickedUp = true;

      HidePickupObject();

      StartCoroutine(SelfDestroyRoutine());
    }

    private void HidePickupObject() =>
      _pickupObject.SetActive(false);

    private IEnumerator SelfDestroyRoutine() {
      yield return new WaitForSeconds(SelfDestroySeconds);

      Destroy(gameObject);
    }
  }
}
