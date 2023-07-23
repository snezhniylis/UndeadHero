using UndeadHero.Infrastructure.States;
using UndeadHero.Infrastructure.Services;

namespace UndeadHero.Infrastructure {
  public class Game {
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen) {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen, GameServices.Container);
    }
  }
}