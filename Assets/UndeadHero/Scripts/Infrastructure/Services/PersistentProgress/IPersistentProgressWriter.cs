using UndeadHero.Data;

namespace UndeadHero.Infrastructure.Services.PersistentProgress {
  public interface IPersistentProgressWriter : IPersistentProgressReader {
    void UpdateProgress(PlayerProgress progress);
  }
}
