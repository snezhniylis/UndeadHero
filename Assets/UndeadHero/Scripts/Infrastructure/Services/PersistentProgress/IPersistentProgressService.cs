using UndeadHero.Data;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.PersistentProgress {
  public interface IPersistentProgressService {
    PlayerProgress LoadSavedProgress();
    void SaveLevelProgress();
    void RestoreLevelProgress();
    void CleanUp();
    void BindSceneObject(GameObject gameObject);
  }
}
