using UnityEngine;

namespace UndeadHero.Infrastructure.Services.Input {
  public class DebugInputService : MobileInputService {
    public override Vector2 MovementAxis {
      get {
        Vector2 axis = base.MovementAxis;
        return (axis == Vector2.zero) ? GetStandaloneInputAxis() : axis;
      }
    }

    private static Vector2 GetStandaloneInputAxis() {
      return new Vector2(UnityEngine.Input.GetAxis(XAxisId), UnityEngine.Input.GetAxis(YAxisId));
    }
  }
}