using UnityEngine;

namespace UndeadHero.Infrastructure {
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner {
    private Game _game;

    private void Awake() {
      _game = new Game(this);
      _game.StateMachine.Enter<StateBootstrap>();

      DontDestroyOnLoad(this);
    }
  }
}