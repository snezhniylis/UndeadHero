using System;
using System.Collections;
using UnityEngine;

namespace UndeadHero.Level.Pickups {
  public class PickUpTrigger : MonoBehaviour {
    private const float SelfDestroySeconds = 1f;

    public Action<Collider> OnHeroPickUp;

    private bool _wasPickedUp;

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

      StartCoroutine(SelfDestroyRoutine());
    }

    private IEnumerator SelfDestroyRoutine() {
      yield return new WaitForSeconds(SelfDestroySeconds);

      Destroy(gameObject);
    }
  }
}
