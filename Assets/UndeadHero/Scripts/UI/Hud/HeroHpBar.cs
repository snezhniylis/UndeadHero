using UnityEngine;
using UnityEngine.UI;

namespace UndeadHero.UI.Hud {
  public class HeroHpBar : MonoBehaviour {
    [SerializeField]
    private Image _barFill;

    public void UpdateBar(float currentHealth, float maxHealth) {
      _barFill.fillAmount = currentHealth / maxHealth;
    }
  }
}
