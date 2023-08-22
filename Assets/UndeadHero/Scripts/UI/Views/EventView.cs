using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.ViewManagement;

namespace UndeadHero.UI.Views {
  public abstract class EventView : View {
    protected GameEvent GameEvent { get; private set; }
    protected HeroInventory HeroInventory { get; private set; }

    public void Initialize(IViewManager viewManager, GameEvent gameEvent, HeroInventory heroInventory) {
      base.Initialize(viewManager);
      GameEvent = gameEvent;
      HeroInventory = heroInventory;
    }
  }
}
