using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.UiFactory;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UnityEngine;

namespace UndeadHero.UI.Hud {
  public class PlayerHud : MonoBehaviour {
    [SerializeField] private HpBar _hpBar;
    [SerializeField] private EssenceCounter _essenceCounter;
    [SerializeField] private Transform _eventButtonsRoot;

    private IUiFactory _uiFactory;

    public void Initialize(HeroHealth heroHealth, HeroInventory heroInventory, IUiFactory uiFactory) {
      _hpBar.Initialize(heroHealth);
      _essenceCounter.Initialize(heroInventory);

      _uiFactory = uiFactory;
    }

    public void AddEventButton(GameEvent gameEvent) =>
      _uiFactory.CreateEventButton(gameEvent, _eventButtonsRoot);
  }
}
