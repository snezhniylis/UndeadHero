using System;
using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace UndeadHero.Character.Hero {
  public class HeroInventory : MonoBehaviour, IPersistentProgressWriter {
    private int _essence;

    public int Essence {
      get => _essence;
      private set {
        _essence = value;
        OnEssenceAmountChanged?.Invoke(value);
      }
    }

    public Action<int> OnEssenceAmountChanged;

    public void ReadProgress(PlayerProgress progress) =>
      Essence = progress.PlayerStats.Essence;

    public void WriteProgress(PlayerProgress progress) =>
      progress.PlayerStats.Essence = Essence;

    public void AddEssence(int amount) =>
      Essence += amount;

    public void WithdrawEssence(int amount) =>
      Essence -= amount;
  }
}
