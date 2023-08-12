using System;
using UndeadHero.Character.Enemy;
using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.Factory;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.StaticData;
using UnityEngine;

namespace UndeadHero.Level.Spawning {
  public class EnemySpawner : MonoBehaviour, IPersistentProgressWriter {
    [SerializeField] private EnemyTypeId _enemyTypeId;

    [field: SerializeField] public string Id { get; private set; }

    private IGameFactory _gameFactory;
    private GameObject _hero;

    private bool _isDefeated;

    public void Initialize(IGameFactory gameFactory, GameObject hero) {
      _gameFactory = gameFactory;
      _hero = hero;
    }

    public void ReadProgress(PlayerProgress progress) {
      _isDefeated = progress != null && progress.WorldData.DefeatedSpawners.Contains(Id);
      if (!_isDefeated) {
        SpawnAssignedEnemy();
      }
    }

    public void WriteProgress(PlayerProgress progress) {
      if (_isDefeated && !progress.WorldData.DefeatedSpawners.Contains(Id)) {
        progress.WorldData.DefeatedSpawners.Add(Id);
      }
    }

    public void GenerateNewId() =>
      Id = Guid.NewGuid().ToString("N");

    private void SpawnAssignedEnemy() {
      Transform spawnPoint = transform;
      GameObject enemy = _gameFactory.CreateEnemy(_enemyTypeId, spawnPoint.position, spawnPoint.rotation, _hero);
      enemy.GetComponent<EnemyDeath>().OnDied += () => _isDefeated = true;
    }
  }
}
