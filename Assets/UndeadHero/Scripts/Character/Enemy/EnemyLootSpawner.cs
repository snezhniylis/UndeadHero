using UndeadHero.Infrastructure.Services.Factory;
using UndeadHero.Infrastructure.Services.Random;
using UndeadHero.Level.Pickups;
using UnityEngine;

namespace UndeadHero.Character.Enemy {
  public class EnemyLootSpawner : MonoBehaviour {
    [SerializeField] private EnemyDeath _enemyDeath;

    private IGameFactory _gameFactory;
    private IRandomizer _randomizer;

    private int _minLootValue;
    private int _maxLootValue;

    public void Initialize(int minLootValue, int maxLootValue, IGameFactory gameFactory, IRandomizer randomizer) {
      _minLootValue = minLootValue;
      _maxLootValue = maxLootValue;
      _gameFactory = gameFactory;
      _randomizer = randomizer;
    }

    private void Start() =>
      _enemyDeath.OnDied += SpawnLoot;

    private void SpawnLoot() {
      GameObject enemyLootContainer = _gameFactory.CreateEnemyLootContainer(transform.position);
      var itemContainer = enemyLootContainer.GetComponent<ItemContainer>();

      int rewardEssence = _randomizer.Next(_minLootValue, _maxLootValue);

      itemContainer.StoreEssence(rewardEssence);
    }
  }
}
