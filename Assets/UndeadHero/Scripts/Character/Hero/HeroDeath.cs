using UndeadHero.Character.Base;
using UnityEngine;

namespace UndeadHero.Character.Hero {
  [RequireComponent(typeof(HeroMover))]
  public class HeroDeath : CharacterDeath {
    [SerializeField] private HeroMover _heroMover;

    protected override void ApplyDeathEffect() =>
      _heroMover.enabled = false;
  }
}
