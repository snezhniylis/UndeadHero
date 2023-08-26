using UnityEngine;
using UnityEngine.SceneManagement;

namespace UndeadHero.Infrastructure {
  public class GameStartup : MonoBehaviour {
    private const string EntrySceneName = "Entry";

    private void Awake() {
#if UNITY_EDITOR
      if (FindObjectOfType<Game>() == null) {
        SceneManager.LoadScene(EntrySceneName);
      }
#endif
    }
  }
}
