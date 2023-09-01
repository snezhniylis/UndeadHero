using System;

namespace UndeadHero.Infrastructure.Services.Ads {
  public interface IAdService {
    void Initialize();
    bool CanShowRewardedVideo();
    void ShowRewardedVideo(Action callback);
    event Action<bool> OnRewardedVideoAvailabilityChanged;
  }
}
