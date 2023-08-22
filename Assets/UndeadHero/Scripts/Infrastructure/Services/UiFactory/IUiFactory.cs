using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.StaticData;
using UndeadHero.UI.Views;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.UiFactory {
  public interface IUiFactory : IService {
    Transform CreateUiRoot();
    GameObject CreateEventButton(GameEvent gameEvent, Transform parent, IViewManager viewManager, HeroInventory heroInventory);
    EventView CreateEventView(ViewId viewId, Transform parent, ViewManager viewManager, GameEvent gameEvent, HeroInventory heroInventory);
  }
}
