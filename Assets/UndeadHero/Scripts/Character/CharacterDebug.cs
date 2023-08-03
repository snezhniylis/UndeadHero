using UnityEngine;

namespace UndeadHero.Character {
  public static class CharacterDebug {
    public static void DrawRaysTimed(Vector3 worldPosition, float radius, float displayTime) {
      Debug.DrawRay(worldPosition, radius * Vector3.up, Color.red, displayTime);
      Debug.DrawRay(worldPosition, radius * Vector3.down, Color.red, displayTime);
      Debug.DrawRay(worldPosition, radius * Vector3.left, Color.red, displayTime);
      Debug.DrawRay(worldPosition, radius * Vector3.right, Color.red, displayTime);
      Debug.DrawRay(worldPosition, radius * Vector3.forward, Color.red, displayTime);
      Debug.DrawRay(worldPosition, radius * Vector3.back, Color.red, displayTime);
    }
  }
}
