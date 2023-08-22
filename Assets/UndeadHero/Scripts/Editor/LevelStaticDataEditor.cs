using System.Linq;
using UndeadHero.Level.Spawning;
using UndeadHero.StaticData.Levels;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UndeadHero.Scripts.Editor {
  [CustomEditor(typeof(LevelStaticData))]
  public class LevelStaticDataEditor : UnityEditor.Editor {
    public override void OnInspectorGUI() {
      base.OnInspectorGUI();

      var levelData = (LevelStaticData)target;

      if (GUILayout.Button("Collect")) {
        levelData.EnemySpawners = FindObjectsOfType<EnemySpawnMarker>()
          .Select(x => new EnemySpawnerData() {
            EnemyTypeId = x.EnemyTypeId,
            SpawnerId = x.SpawnerId,
            Position = x.transform.position
          }).ToList();

        levelData.LevelName = SceneManager.GetActiveScene().name;

        EditorUtility.SetDirty(target);
      }
    }
  }
}
