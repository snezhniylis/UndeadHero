using UndeadHero.Character.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UndeadHero.UI.Hud {
  public class HpBar : MonoBehaviour {
    [SerializeField] private Image _barFill;

    private CharacterHealth _characterHealth;

    public void Initialize(CharacterHealth characterHealth) {
      _characterHealth = characterHealth;
      _characterHealth.OnHealthChanged += UpdateBar;
    }

    private void UpdateBar() =>
      _barFill.fillAmount = _characterHealth.Current / _characterHealth.Max;
  }
}
