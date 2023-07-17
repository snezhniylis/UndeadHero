using UnityEngine;

namespace UndeadHero.Services.Input {
  public interface IInputService {
    Vector2 MovementAxis { get; }
  }
}