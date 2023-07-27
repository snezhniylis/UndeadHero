using UnityEngine;
using UndeadHero.Infrastructure.States;

namespace UndeadHero.Infrastructure {
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner {
    [SerializeField]
    private LoadingScreen _loadingScreen;

    private Game _game;

    private void Awake() {
      _game = new Game(this, _loadingScreen);
      _game.StateMachine.Enter<StateBootstrap>();

      DontDestroyOnLoad(this);
    }
  }
}
