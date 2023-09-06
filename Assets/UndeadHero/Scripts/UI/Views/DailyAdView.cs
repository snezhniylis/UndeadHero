using UndeadHero.Character.Hero;
using UndeadHero.Events;
using UndeadHero.Infrastructure.Services.Ads;
using UndeadHero.Infrastructure.Services.Events;
using UndeadHero.Infrastructure.Services.SceneObjectsRegistry;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.StaticData.Events;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace UndeadHero.UI.Views {
  public class DailyAdView : View {
    private const EventId RelatedEventId = EventId.DailyAd;

    [SerializeField] private Button _showAdButton;

    private IAdService _adService;

    private HeroInventory _heroInventory;
    private GameEvent _dailyAdEvent;

    [Inject]
    public void Initialize(IViewManager viewManager, IEventRegistry eventRegistry, IAdService adService, ISceneObjectsRegistry sceneObjects) {
      base.Initialize(viewManager);

      _adService = adService;

      _heroInventory = sceneObjects.Hero.GetComponent<HeroInventory>();

      _dailyAdEvent = eventRegistry.GetEvent(RelatedEventId);

      _showAdButton.onClick.AddListener(() => _adService.ShowRewardedVideo(OnAdSuccessfullyWatched));
    }

    protected override void OnShow() {
      _adService.OnRewardedVideoAvailabilityChanged += ChangeAdButtonState;
      ChangeAdButtonState(_adService.CanShowRewardedVideo());
    }

    protected override void OnHide() =>
      _adService.OnRewardedVideoAvailabilityChanged -= ChangeAdButtonState;

    private void OnAdSuccessfullyWatched() {
      GrantReward();

      _dailyAdEvent.MarkFinished();

      CloseView();
    }

    private void GrantReward() {
      _heroInventory.AddEssence(100);
    }

    private void ChangeAdButtonState(bool isEnabled) {
      _showAdButton.interactable = isEnabled;
    }
  }
}
