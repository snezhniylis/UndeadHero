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

    public static void DrawImpactSphere(Vector3 origin, float radius) {
      Gizmos.color = new Color32(20, 255, 30, 170);
      Gizmos.DrawSphere(origin, 0.1f);
      Gizmos.color = new Color32(20, 255, 30, 80);
      Gizmos.DrawSphere(origin, radius);
    }
  }
}
