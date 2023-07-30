using UnityEngine;

namespace UndeadHero.Character.Animation {
  public class AnimatorStateReporter : StateMachineBehaviour {
    private IAnimationStateReader _stateReader;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      base.OnStateEnter(animator, stateInfo, layerIndex);
      FindReader(animator);
      _stateReader.OnStateEntered(stateInfo.shortNameHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
      base.OnStateExit(animator, stateInfo, layerIndex);
      FindReader(animator);
      _stateReader.OnStateExited(stateInfo.shortNameHash);
    }

    private void FindReader(Animator animator) =>
      _stateReader ??= animator.gameObject.GetComponent<IAnimationStateReader>();
  }
}
