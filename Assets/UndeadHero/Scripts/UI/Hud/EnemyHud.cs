using UndeadHero.Character;
using UnityEngine;

namespace UndeadHero.UI.Hud {
  public class EnemyHud : MonoBehaviour {
    [SerializeField]
    private HpBar _hpBar;
    [SerializeField]
    private CharacterHealth _characterHealth;

    private void Start() =>
      _hpBar.Initialize(_characterHealth);
  }
}
