using System;

namespace UndeadHero.Infrastructure.Services.SceneLoading {
  public interface ISceneLoader {
    void Load(string name, Action onLoaded = null);
  }
}
