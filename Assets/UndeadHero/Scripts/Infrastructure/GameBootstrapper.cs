using UnityEngine;
using UndeadHero.Infrastructure.States;

namespace UndeadHero.Infrastructure {
  public class GameBootstrapper : MonoBehaviour, ICoroutineRunner {
    [SerializeField]
    private LoadingScreen _loadingScreenPrefab;

    private Game _game;

    private void Awake() {
      _game = new Game(this, Instantiate(_loadingScreenPrefab));
      _game.StateMachine.Enter<StateBootstrap>();

      DontDestroyOnLoad(this);
    }
  }
}
