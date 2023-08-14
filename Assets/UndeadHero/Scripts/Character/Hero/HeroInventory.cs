using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace UndeadHero.Character.Hero {
  public class HeroInventory : MonoBehaviour, IPersistentProgressWriter {
    public int Essence { get; private set; }

    public void ReadProgress(PlayerProgress progress) {
      if (progress != null) {
        Essence = progress.HeroData.Essence;
      }
    }

    public void WriteProgress(PlayerProgress progress) {
      if (progress != null) {
        progress.HeroData.Essence = Essence;
      }
    }

    public void AddEssence(int amount) =>
      Essence += amount;
  }
}
