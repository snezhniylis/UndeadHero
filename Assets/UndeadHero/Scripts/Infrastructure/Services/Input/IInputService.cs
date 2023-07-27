using UnityEngine;
using UndeadHero.Infrastructure.Services;

namespace UndeadHero.Infrastructure.Services.Input {
  public interface IInputService : IService {
    Vector2 MovementAxis { get; }
  }
}
