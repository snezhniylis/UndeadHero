using UnityEngine;

namespace UndeadHero.Infrastructure {
  public class GameStartup : MonoBehaviour {
    public GameBootstrapper BootstrapperPrefab;

    private void Awake() {
      if (FindObjectOfType<GameBootstrapper>() == null) {
        Instantiate(BootstrapperPrefab);
      }
    }
  }
}
