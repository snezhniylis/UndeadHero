using UndeadHero.Character.Hero;
using UnityEngine;

namespace UndeadHero.UI.Hud {
  public class PlayerHud : MonoBehaviour {
    [SerializeField] private HpBar _hpBar;
    [SerializeField] private EssenceCounter _essenceCounter;

    public void Initialize(HeroHealth heroHealth, HeroInventory heroInventory) {
      _hpBar.Initialize(heroHealth);
      _essenceCounter.Initialize(heroInventory);
    }
  }
}
