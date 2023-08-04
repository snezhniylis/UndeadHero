using UndeadHero.Character.Hero;
using UnityEngine;

namespace UndeadHero.UI.Hud {
  public class Hud : MonoBehaviour {
    [SerializeField]
    private HeroHpBar _heroHpBar;

    private HeroHealth _heroHealth;

    private void OnDestroy() {
      _heroHealth.OnHealthChanged -= UpdateHpBar;
    }

    public void Initialize(HeroHealth heroHealth) {
      _heroHealth = heroHealth;
      _heroHealth.OnHealthChanged += UpdateHpBar;
    }

    private void UpdateHpBar() =>
      _heroHpBar.UpdateBar(_heroHealth.Current, _heroHealth.Max);
  }
}
