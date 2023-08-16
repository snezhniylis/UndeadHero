using TMPro;
using UndeadHero.Character.Hero;
using UnityEngine;

namespace UndeadHero.UI.Hud {
  public class EssenceCounter : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _counter;

    public void Initialize(HeroInventory heroInventory) {
      heroInventory.OnEssenceAmountChanged += UpdateCounter;
      UpdateCounter(heroInventory.Essence);
    }

    private void UpdateCounter(int essenceAmount) =>
      _counter.text = essenceAmount.ToString();
  }
}
