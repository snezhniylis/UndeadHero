using UndeadHero.Character.Base;
using UnityEngine;

namespace UndeadHero.UI.Hud {
  public class UnitHud : MonoBehaviour {
    [SerializeField] private HpBar _hpBar;
    [SerializeField] private CharacterHealth _characterHealth;

    private void Start() =>
      _hpBar.Initialize(_characterHealth);
  }
}
