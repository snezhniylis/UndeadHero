using UndeadHero.Services.Input;
using UndeadHero.Infrastructure.States;

namespace UndeadHero.Infrastructure {
  public class Game {
    public static IInputService InputService;
    public GameStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingScreen loadingScreen) {
      StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), loadingScreen);
    }
  }
}