using UndeadHero.Data;

namespace UndeadHero.Infrastructure.Services.PersistentProgress {
  public interface IPersistentProgressService : IService {
    PlayerProgress Progress { get; }

    void InitializeProgress();
    void SaveProgress();
    void LoadProgress();
    void AddSubscriber(IPersistentProgressReader progressReader);
    void ClearSubscribers();
  }
}
