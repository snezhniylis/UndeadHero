using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.Events;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.StaticData.Events;

namespace UndeadHero.UI.Views {
  public class DailyAdView : View {
    private const EventId RelatedEventId = EventId.DailyAd;

    public void Initialize(IViewManager viewManager, IEventRegistry eventRegistry, HeroInventory heroInventory) {
      base.Initialize(viewManager);

      GameEvent gameEvent = eventRegistry.GetEvent(RelatedEventId);
    }
  }
}
