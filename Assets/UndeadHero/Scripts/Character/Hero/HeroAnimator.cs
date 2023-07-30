using UnityEngine;

namespace UndeadHero.Character.Hero {
  [RequireComponent(typeof(Animator))]
  [RequireComponent(typeof(CharacterController))]
  public class HeroAnimator : MonoBehaviour {
    private static readonly int StateRunningHash = Animator.StringToHash("stateRunning");

    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private CharacterController _characterController;

    private void OnValidate() {
      _animator = GetComponent<Animator>();
      _characterController = GetComponent<CharacterController>();
    }

    private void Update() {
      UpdateCharacterRunningState();
    }

    private void UpdateCharacterRunningState() {
      Vector3 horizontalCharacterVelocity = new(_characterController.velocity.x, 0, _characterController.velocity.z);
      bool isCharacterRunning = horizontalCharacterVelocity.magnitude > 0.01;
      _animator.SetBool(StateRunningHash, isCharacterRunning);
    }
  }
}
