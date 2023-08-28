using UndeadHero.Character.Base;
using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace UndeadHero.Character.Hero {
  [RequireComponent(typeof(HeroAnimator))]
  public class HeroHealth : CharacterHealth, IPersistentProgressWriter {
    public void ReadProgress(PlayerProgress progress) {
      if (progress.CurrentLevel != null) {
        Current = progress.CurrentLevel.PlayerHp;
      }
    }

    public void WriteProgress(PlayerProgress progress) =>
      progress.CurrentLevel.PlayerHp = Current;
  }
}
