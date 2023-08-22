using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.StaticData;
using UndeadHero.UI.Views;

namespace UndeadHero.Infrastructure.Services.ViewManagement {
  public interface IViewManager : IService {
    void Open(ViewId viewId, GameEvent gameEvent, HeroInventory heroInventory);
    void CloseActive();
    void SpawnUiRoot();
    void CleanUp();
  }
}
