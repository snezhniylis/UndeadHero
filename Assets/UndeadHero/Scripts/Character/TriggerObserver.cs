using System;
using UnityEngine;

namespace UndeadHero.Character {
  [RequireComponent(typeof(Collider))]
  public class TriggerObserver : MonoBehaviour {
    public event Action<Collider> OnEnteredTrigger;
    public event Action<Collider> OnExitedTrigger;

    private void OnTriggerEnter(Collider other) =>
      OnEnteredTrigger?.Invoke(other);

    private void OnTriggerExit(Collider other) =>
      OnExitedTrigger?.Invoke(other);
  }
}
