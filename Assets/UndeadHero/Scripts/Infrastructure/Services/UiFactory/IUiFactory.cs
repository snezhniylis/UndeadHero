using UndeadHero.Events;
using UndeadHero.UI.Views;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.UiFactory {
  public interface IUiFactory {
    Transform CreateUiRoot();
    GameObject CreateEventButton(GameEvent gameEvent, Transform parent);
    View CreateView(ViewId viewId, Transform parent);
  }
}
