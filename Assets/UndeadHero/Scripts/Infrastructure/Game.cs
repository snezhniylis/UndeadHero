using UndeadHero.Infrastructure.States;
using UndeadHero.Infrastructure.Services;
using UndeadHero.UI.LoadingScreen;

namespace UndeadHero.Infrastructure {
  public class Game {
    public readonly GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen) {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, GameServices.Container);
    }
  }
}
