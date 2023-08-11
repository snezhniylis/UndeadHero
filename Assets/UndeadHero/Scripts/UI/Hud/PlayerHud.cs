using UndeadHero.Character.Hero;
using UnityEngine;

namespace UndeadHero.UI.Hud {
  public class PlayerHud : MonoBehaviour {
    [SerializeField] private HpBar _hpBar;

    public void Initialize(HeroHealth heroHealth) =>
      _hpBar.Initialize(heroHealth);
  }
}
