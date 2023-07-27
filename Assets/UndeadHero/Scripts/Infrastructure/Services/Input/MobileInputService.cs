using UnityEngine;

namespace UndeadHero.Infrastructure.Services.Input {
  public class MobileInputService : IInputService {
    protected const string XAxisId = "Horizontal";
    protected const string YAxisId = "Vertical";

    public virtual Vector2 MovementAxis =>
      new(SimpleInput.GetAxis(XAxisId), SimpleInput.GetAxis(YAxisId));
  }
}