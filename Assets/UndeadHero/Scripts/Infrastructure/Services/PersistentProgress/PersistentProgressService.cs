using System.Collections.Generic;
using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.SaveManagement;

namespace UndeadHero.Infrastructure.Services.PersistentProgress {
  public class PersistentProgressService : IPersistentProgressService {
    private const string FirstPlayableSceneName = "Cemetery";
    private const string ProgressKey = "Progress";

    private readonly List<IPersistentProgressReader> _progressReaders = new();
    private readonly List<IPersistentProgressWriter> _progressWriters = new();

    private readonly ISaveManager _saveManager;

    public PlayerProgress Progress { get; private set; }

    public PersistentProgressService(ISaveManager saveManager) {
      _saveManager = saveManager;
    }

    public void InitializeProgress() =>
      Progress = _saveManager.Load<PlayerProgress>(ProgressKey) ?? new PlayerProgress(FirstPlayableSceneName);

    public void SaveProgress() {
      UpdateProgress();
      _saveManager.Save(ProgressKey, Progress);
    }

    public void LoadProgress() {
      foreach (IPersistentProgressReader progressReader in _progressReaders) {
        progressReader.LoadProgress(Progress);
      }
    }

    public void AddSubscriber(IPersistentProgressReader progressReader) {
      if (progressReader is IPersistentProgressWriter progressWriter) {
        _progressWriters.Add(progressWriter);
      }

      _progressReaders.Add(progressReader);
    }

    public void ClearSubscribers() {
      _progressReaders.Clear();
      _progressWriters.Clear();
    }

    private void UpdateProgress() {
      foreach (IPersistentProgressWriter progressWriter in _progressWriters) {
        progressWriter.UpdateProgress(Progress);
      }
    }
  }
}
