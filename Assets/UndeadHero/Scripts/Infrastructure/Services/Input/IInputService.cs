using UnityEngine;

namespace UndeadHero.Infrastructure.Services.Input {
  public interface IInputService {
    Vector2 MovementAxis { get; }
    bool AttackTriggered { get; }
  }
}
