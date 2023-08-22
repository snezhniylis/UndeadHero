using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UndeadHero.UI.Elements {
  public class OpenEventViewButton : MonoBehaviour {
    [SerializeField] private Image _eventIcon;
    [SerializeField] private Button _eventButton;

    public void Initialize(GameEvent gameEvent, IViewManager viewManager, HeroInventory heroInventory) {
      _eventIcon.sprite = gameEvent.Icon;
      _eventButton.onClick.AddListener(() => viewManager.Open(gameEvent.ViewId, gameEvent, heroInventory));
      gameEvent.OnEventCompleted += () => gameObject.SetActive(false);
    }
  }
}
