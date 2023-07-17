using UnityEngine;
using UndeadHero.Services.Input;

namespace UndeadHero.Infrastructure {
  public class Game {
    public static IInputService InputService { get; private set; }

    public Game() {
      InputService = InitializeInputService();
    }

    private static IInputService InitializeInputService() {
      return Application.isEditor ? new DebugInputService() : new MobileInputService();
    }
  }
}