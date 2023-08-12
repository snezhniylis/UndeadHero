using UndeadHero.Data;

namespace UndeadHero.Infrastructure.Services.PersistentProgress {
  public interface IPersistentProgressReader {
    void ReadProgress(PlayerProgress progress);
  }
}
