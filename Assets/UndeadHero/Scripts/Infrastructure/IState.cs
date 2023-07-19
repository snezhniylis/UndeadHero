namespace UndeadHero.Infrastructure {
  public interface IState {
    void Enter();
    void Exit();
  }
}