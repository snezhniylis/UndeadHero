using UnityEngine;

namespace UndeadHero.StaticData {
  [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/Enemy")]
  public class EnemyStaticData : ScriptableObject {
    public EnemyTypeId EnemyTypeId;
    public GameObject Prefab;

    [Header("Stats")] [Range(1f, 100f)] public int Hp;
    [Range(0, 10f)] public float MovementSpeed;

    [Header("Attack")] [Range(1, 100)] public float AttackDamage;
    [Range(0, 10f)] public float AttackCooldown;
    public Vector3 AttackImpactOrigin;
    [Range(0, 2f)] public float AttackImpactRadius;

    [Header("Loot")] [Range(0, 100)] public int MinLootValue;
    [Range(0, 100)] public int MaxLootValue;
  }
}
