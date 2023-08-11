using UnityEngine;

namespace UndeadHero.Infrastructure.Services.Input {
  public interface IInputService : IService {
    Vector2 MovementAxis { get; }
    bool AttackTriggered { get; }
  }
}
