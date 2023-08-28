using System.Collections.Generic;
using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.SaveManagement;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.PersistentProgress {
  public class PersistentProgressService : IPersistentProgressService {
    private const string ProgressKey = "Progress";

    private readonly List<IPersistentProgressReader> _progressReaders = new();
    private readonly List<IPersistentProgressWriter> _progressWriters = new();

    private readonly ISaveManager _saveManager;

    private PlayerProgress _progress;

    public PersistentProgressService(ISaveManager saveManager) {
      _saveManager = saveManager;
    }

    public PlayerProgress LoadSavedProgress() =>
      _progress = _saveManager.Load<PlayerProgress>(ProgressKey) ?? new PlayerProgress();

    public void SaveLevelProgress() {
      _progress.CurrentLevel = new CurrentLevelData();
      ActualizeProgress();
      _saveManager.Save(ProgressKey, _progress);
    }

    public void RestoreLevelProgress() {
      foreach (IPersistentProgressReader progressReader in _progressReaders) {
        progressReader.ReadProgress(_progress);
      }
    }

    public void BindSceneObject(GameObject gameObject) {
      foreach (IPersistentProgressReader progressReader in gameObject.GetComponentsInChildren<IPersistentProgressReader>()) {
        if (progressReader is IPersistentProgressWriter progressWriter) {
          _progressWriters.Add(progressWriter);
        }

        _progressReaders.Add(progressReader);
      }
    }

    public void CleanUp() {
      _progressReaders.Clear();
      _progressWriters.Clear();
    }

    private void ActualizeProgress() {
      foreach (IPersistentProgressWriter progressWriter in _progressWriters) {
        progressWriter.WriteProgress(_progress);
      }
    }
  }
}
