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
    private IViewManager _viewManager;

    private HeroInventory _heroInventory;

    public void Initialize(HeroHealth heroHealth, HeroInventory heroInventory, IUiFactory uiFactory, IViewManager viewManager) {
      _hpBar.Initialize(heroHealth);
      _essenceCounter.Initialize(heroInventory);

      _heroInventory = heroInventory;

      _uiFactory = uiFactory;
      _viewManager = viewManager;
    }

    public void AddEventButton(GameEvent gameEvent) =>
      _uiFactory.CreateEventButton(gameEvent, _eventButtonsRoot, _viewManager, _heroInventory);
  }
}
