using UnityEngine;
using UndeadHero.Character.Animation;

namespace UndeadHero.Character.Hero {
  [RequireComponent(typeof(CharacterController))]
  public class HeroAnimator : CharacterAnimator {
    [SerializeField]
    private CharacterController _characterController;

    private void OnValidate() {
      _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
      AnimateHeroMovement();
    }

    private void AnimateHeroMovement() {
      Vector3 heroVelocity = _characterController.velocity;
      Vector3 horizontalHeroVelocity = new(heroVelocity.x, 0, heroVelocity.z);
      float heroSpeed = horizontalHeroVelocity.magnitude;
      if (heroSpeed > 0.01) {
        Move(heroSpeed);
      }
      else {
        StopMoving();
      }
    }
  }
}
