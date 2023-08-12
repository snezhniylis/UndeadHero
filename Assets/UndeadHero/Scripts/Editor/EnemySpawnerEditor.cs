using System.Linq;
using UndeadHero.Level.Spawning;
using UnityEditor;
using UnityEngine;

namespace UndeadHero.Scripts.Editor {
  [CustomEditor(typeof(EnemySpawner))]
  public class EnemySpawnerEditor : UnityEditor.Editor {
    private const float GizmoSphereRadius = 0.5f;

    [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
    public static void DrawSpawnerGizmos(EnemySpawner spawner, GizmoType gizmoType) {
      Gizmos.color = Color.green;
      Gizmos.DrawSphere(spawner.transform.position, GizmoSphereRadius);
    }

    private void OnEnable() =>
      EnsureUniqueSpawnerId((EnemySpawner)target);

    private static void EnsureUniqueSpawnerId(EnemySpawner spawner) {
      if (string.IsNullOrEmpty(spawner.Id)) {
        ChangeSpawnerId(spawner);
      }
      else {
        var otherSpawners = FindObjectsOfType<EnemySpawner>();
        if (otherSpawners.Any(anotherSpawner => anotherSpawner != spawner && anotherSpawner.Id == spawner.Id)) {
          ChangeSpawnerId(spawner);
        }
      }
    }

    private static void ChangeSpawnerId(EnemySpawner spawner) {
      spawner.GenerateNewId();
      if (!Application.isPlaying) {
        EditorUtility.SetDirty(spawner);
      }
    }
  }
}
