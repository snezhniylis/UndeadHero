using UnityEngine;
using UndeadHero.Infrastructure.Services;

namespace UndeadHero.Services.Input {
  public interface IInputService : IService {
    Vector2 MovementAxis { get; }
  }
}