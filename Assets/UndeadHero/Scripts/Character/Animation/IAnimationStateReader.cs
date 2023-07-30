namespace UndeadHero.Character.Animation {
  public interface IAnimationStateReader {
    void OnStateEntered(int stateHash);
    void OnStateExited(int stateHash);
  }
}
