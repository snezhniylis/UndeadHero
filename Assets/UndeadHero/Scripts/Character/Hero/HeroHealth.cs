using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace UndeadHero.Character.Hero {
  [RequireComponent(typeof(HeroAnimator))]
  public class HeroHealth : CharacterHealth, IPersistentProgressWriter {
    public void LoadProgress(PlayerProgress progress) {
      Max = progress.HeroData.MaxHp;
      Current = progress.HeroData.CurrentHp;
    }

    public void UpdateProgress(PlayerProgress progress) {
      progress.HeroData.MaxHp = Max;
      progress.HeroData.CurrentHp = Current;
    }
  }
}
