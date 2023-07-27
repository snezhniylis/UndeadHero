namespace UndeadHero.Infrastructure.States {
  public interface IStateBase {
    void Exit();
  }

  public interface IState : IStateBase {
    void Enter();
  }

  public interface IStatePayloaded<TPayload> : IStateBase {
    void Enter(TPayload payload);
  }
}
