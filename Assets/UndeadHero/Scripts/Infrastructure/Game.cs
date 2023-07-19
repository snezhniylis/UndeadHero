using UnityEngine;
using UndeadHero.Services.Input;

namespace UndeadHero.Infrastructure {
  public class Game {
    public static IInputService InputService;
    public GameStateMachine StateMachine;

    public Game() {
      StateMachine = new GameStateMachine();
    }
  }
}