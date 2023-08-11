using System;
using UnityEngine;

namespace UndeadHero.Character.Base.Animation {
  [RequireComponent(typeof(Animator))]
  public abstract class CharacterAnimator : MonoBehaviour, IAnimationStateReader {
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int Speed = Animator.StringToHash("speed");
    private static readonly int DieTrigger = Animator.StringToHash("die");
    private static readonly int AttackTrigger = Animator.StringToHash("attack");
    private static readonly int GetHitTrigger = Animator.StringToHash("getHit");

    private static readonly int IdleState = Animator.StringToHash("Idle");
    private static readonly int MoveState = Animator.StringToHash("Move");
    private static readonly int DieState = Animator.StringToHash("Die");
    private static readonly int AttackState = Animator.StringToHash("Attack");

    [SerializeField] private Animator _animator;

    public AnimatorState State { get; private set; }

    public event Action<AnimatorState> OnStateEntered;
    public event Action<AnimatorState> OnStateExited;

    public void BroadcastStateEntered(int stateHash) {
      State = HashToState(stateHash);
      OnStateEntered?.Invoke(State);
    }

    public void BroadcastStateExited(int stateHash) =>
      OnStateExited?.Invoke(HashToState(stateHash));

    public void Move(float speed) {
      _animator.SetBool(IsMoving, true);
      _animator.SetFloat(Speed, speed);
    }

    public void StopMoving() => _animator.SetBool(IsMoving, false);

    public void Die() => _animator.SetTrigger(DieTrigger);

    public void Attack() => _animator.SetTrigger(AttackTrigger);

    public void GetHit() => _animator.SetTrigger(GetHitTrigger);

    private static AnimatorState HashToState(int stateHash) {
      if (stateHash == IdleState)
        return AnimatorState.Idle;
      if (stateHash == MoveState)
        return AnimatorState.Moving;
      if (stateHash == DieState)
        return AnimatorState.Dead;
      if (stateHash == AttackState)
        return AnimatorState.Attacking;

      return AnimatorState.Unknown;
    }
  }
}
