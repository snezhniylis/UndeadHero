namespace UndeadHero.Character.Base.Animation {
  public interface IAnimationStateReader {
    void BroadcastStateEntered(int stateHash);
    void BroadcastStateExited(int stateHash);
  }
}
