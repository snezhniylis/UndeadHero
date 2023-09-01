using System;

namespace UndeadHero.Infrastructure.Services.Ads {
  public class AdService : IAdService {
    // I don't want to waste my time on moving it out to some secrets manager,
    // or even to project static data. I've already spent way too much time on this project.
    private const string IronSourceAppKey = "1b5407c55";

    private bool _isRewardedVideoAvailable;

    public event Action<bool> OnRewardedVideoAvailabilityChanged;

    private Action _onRewardedVideoSuccessfullyWatched;

    public void Initialize() {
      IronSource.Agent.init(IronSourceAppKey, IronSourceAdUnits.REWARDED_VIDEO);
      // IronSource.Agent.validateIntegration();

      IronSourceRewardedVideoEvents.onAdAvailableEvent += (_) => SetRewardedVideoAvailabilityState(true);
      IronSourceRewardedVideoEvents.onAdUnavailableEvent += () => SetRewardedVideoAvailabilityState(false);
      IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewarded;

      // Not implemented:
      // IronSourceEvents.onSdkInitializationCompletedEvent
      // IronSourceRewardedVideoEvents.onAdOpenedEvent
      // IronSourceRewardedVideoEvents.onAdClosedEvent
      // IronSourceRewardedVideoEvents.onAdShowFailedEvent
      // IronSourceRewardedVideoEvents.onAdClickedEvent
    }

    public bool CanShowRewardedVideo() =>
      _isRewardedVideoAvailable;

    public void ShowRewardedVideo(Action callback) {
      _onRewardedVideoSuccessfullyWatched = callback;

      IronSource.Agent.showRewardedVideo();
    }

    private void RewardedVideoOnAdRewarded(IronSourcePlacement placement, IronSourceAdInfo adInfo) {
      _onRewardedVideoSuccessfullyWatched?.Invoke();
      _onRewardedVideoSuccessfullyWatched = null;
    }

    private void SetRewardedVideoAvailabilityState(bool isAvailable) {
      _isRewardedVideoAvailable = isAvailable;

      OnRewardedVideoAvailabilityChanged?.Invoke(isAvailable);
    }
  }
}
