using System.Linq;
using UndeadHero.Level.Spawning;
using UnityEditor;
using UnityEngine;

namespace UndeadHero.Scripts.Editor {
  [CustomEditor(typeof(EnemySpawnMarker))]
  public class EnemySpawnMarkerEditor : UnityEditor.Editor {
    private const float GizmoSphereRadius = 0.5f;

    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void DrawSpawnerGizmos(EnemySpawnMarker spawnMarker, GizmoType gizmoType) {
      Gizmos.color = Color.green;
      Gizmos.DrawSphere(spawnMarker.transform.position, GizmoSphereRadius);
    }

    private void OnEnable() =>
      EnsureUniqueSpawnerId((EnemySpawnMarker)target);

    private static void EnsureUniqueSpawnerId(EnemySpawnMarker spawnMarker) {
      if (string.IsNullOrEmpty(spawnMarker.SpawnerId)) {
        ChangeSpawnerId(spawnMarker);
      }
      else {
        var otherMarkers = FindObjectsOfType<EnemySpawnMarker>();
        if (otherMarkers.Any(anotherMarker => anotherMarker != spawnMarker && anotherMarker.SpawnerId == spawnMarker.SpawnerId)) {
          ChangeSpawnerId(spawnMarker);
        }
      }
    }

    private static void ChangeSpawnerId(EnemySpawnMarker spawnMarker) {
      spawnMarker.GenerateNewSpawnerId();
      if (!Application.isPlaying) {
        EditorUtility.SetDirty(spawnMarker);
      }
    }
  }
}
