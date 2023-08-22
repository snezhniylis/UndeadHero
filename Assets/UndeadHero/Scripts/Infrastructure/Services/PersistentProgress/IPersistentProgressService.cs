using UndeadHero.Data;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.PersistentProgress {
  public interface IPersistentProgressService : IService {
    PlayerProgress LoadSavedProgress();
    void SaveProgress();
    void RestoreProgress();
    void CleanUp();
    void BindSceneObject(GameObject gameObject);
    void BindObject(IPersistentProgressReader progressReader);
  }
}
