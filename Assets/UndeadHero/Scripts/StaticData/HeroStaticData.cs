using UnityEngine;

namespace UndeadHero.StaticData {
  [CreateAssetMenu(fileName = "HeroData", menuName = "StaticData/Hero", order = 0)]
  public class HeroStaticData : ScriptableObject {
    public GameObject Prefab;

    [Header("Stats")] [Range(1f, 100f)] public int Hp;
    [Range(0, 10f)] public float MovementSpeed;

    [Header("Attack")] [Range(1, 100)] public float AttackDamage;
    [Range(0, 10f)] public float AttackCooldown;
    public Vector3 AttackImpactOrigin;
    [Range(0, 2f)] public float AttackImpactRadius;
  }
}
