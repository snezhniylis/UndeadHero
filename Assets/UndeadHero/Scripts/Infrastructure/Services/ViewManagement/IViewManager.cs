using UndeadHero.UI.Views;

namespace UndeadHero.Infrastructure.Services.ViewManagement {
  public interface IViewManager {
    void Open(ViewId viewId);
    void CloseActive();
    void SpawnUiRoot();
  }
}
