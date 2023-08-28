using UndeadHero.Character.Enemy;
using UndeadHero.Data;
using UndeadHero.Infrastructure.Services.Factory;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.StaticData.Enemies;
using UnityEngine;

namespace UndeadHero.Level.Spawning {
  public class EnemySpawner : MonoBehaviour, IPersistentProgressWriter {
    private string _id;
    private EnemyTypeId _enemyTypeId;
    private IGameFactory _gameFactory;

    private bool _isDefeated;

    public void Initialize(string spawnerId, EnemyTypeId enemyTypeId, IGameFactory gameFactory) {
      _id = spawnerId;
      _enemyTypeId = enemyTypeId;
      _gameFactory = gameFactory;
    }

    public void ReadProgress(PlayerProgress progress) {
      _isDefeated = progress.CurrentLevel != null && progress.CurrentLevel.DefeatedSpawners.Contains(_id);
      if (!_isDefeated) {
        SpawnAssignedEnemy();
      }
    }

    public void WriteProgress(PlayerProgress progress) {
      if (_isDefeated && !progress.CurrentLevel.DefeatedSpawners.Contains(_id)) {
        progress.CurrentLevel.DefeatedSpawners.Add(_id);
      }
    }

    private void SpawnAssignedEnemy() {
      Transform spawnPoint = transform;
      GameObject enemy = _gameFactory.CreateEnemy(_enemyTypeId, spawnPoint.position, spawnPoint.rotation);
      enemy.GetComponent<EnemyDeath>().OnDied += () => _isDefeated = true;
    }
  }
}
