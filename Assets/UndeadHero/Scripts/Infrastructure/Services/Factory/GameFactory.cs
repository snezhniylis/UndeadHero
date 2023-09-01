using UndeadHero.Character.Enemy;
using UndeadHero.Character.Hero;
using UndeadHero.Infrastructure.Services.AssetManagement;
using UndeadHero.Infrastructure.Services.Input;
using UndeadHero.Infrastructure.Services.PersistentProgress;
using UndeadHero.Infrastructure.Services.Random;
using UndeadHero.Infrastructure.Services.SceneObjectsRegistry;
using UndeadHero.Infrastructure.Services.StaticDataManagement;
using UndeadHero.Infrastructure.Services.UiFactory;
using UndeadHero.Level.Spawning;
using UndeadHero.StaticData.Enemies;
using UndeadHero.StaticData.Heroes;
using UndeadHero.UI.Hud;
using UnityEngine;
using UnityEngine.AI;

namespace UndeadHero.Infrastructure.Services.Factory {
  public class GameFactory : IGameFactory {
    private readonly IAssetProvider _assetProvider;
    private readonly IPersistentProgressService _persistentProgress;
    private readonly IStaticDataProvider _staticDataProvider;
    private readonly IRandomizer _randomizer;
    private readonly IUiFactory _uiFactory;
    private readonly IInputService _inputService;
    private readonly ISceneObjectsRegistry _sceneObjects;

    public GameFactory(IAssetProvider assetProvider, IPersistentProgressService persistentProgress, IStaticDataProvider staticDataProvider, IRandomizer randomizer, IUiFactory uiFactory, IInputService inputService, ISceneObjectsRegistry sceneObjects) {
      _assetProvider = assetProvider;
      _persistentProgress = persistentProgress;
      _staticDataProvider = staticDataProvider;
      _randomizer = randomizer;
      _uiFactory = uiFactory;
      _inputService = inputService;
      _sceneObjects = sceneObjects;
    }

    public GameObject CreateHero(Vector3 position) {
      HeroStaticData heroData = _staticDataProvider.GetHeroData();

      GameObject hero = InstantiatePrefab(heroData.Prefab, position, Quaternion.identity);

      hero.GetComponent<HeroHealth>().Initialize(
        heroData.Hp,
        heroData.Hp
      );

      hero.GetComponent<HeroAttack>().Initialize(
        heroData.AttackDamage,
        heroData.AttackCooldown,
        heroData.AttackImpactOrigin,
        heroData.AttackImpactRadius,
        _inputService
      );

      hero.GetComponent<HeroMover>().Initialize(
        heroData.MovementSpeed,
        _inputService
      );

      _sceneObjects.Hero = hero;

      return hero;
    }

    public GameObject CreateEnemy(EnemyTypeId typeId, Vector3 position, Quaternion rotation) {
      EnemyStaticData enemyData = _staticDataProvider.GetEnemyData(typeId);

      GameObject enemy = InstantiatePrefab(enemyData.Prefab, position, rotation);

      enemy.GetComponent<EnemyHealth>().Initialize(
        enemyData.Hp,
        enemyData.Hp
      );

      enemy.GetComponent<EnemyAttack>().Initialize(
        _sceneObjects.Hero.transform,
        enemyData.AttackDamage,
        enemyData.AttackCooldown,
        enemyData.AttackImpactOrigin,
        enemyData.AttackImpactRadius
      );

      enemy.GetComponent<EnemyFollowHero>().Initialize(
        _sceneObjects.Hero.transform
      );

      enemy.GetComponent<EnemyLootSpawner>().Initialize(
        enemyData.MinLootValue,
        enemyData.MaxLootValue,
        this,
        _randomizer
      );

      enemy.GetComponent<NavMeshAgent>().speed = enemyData.MovementSpeed;

      return enemy;
    }

    public GameObject CreateEnemyLootContainer(Vector3 position) =>
      InstantiateByPath(AssetPaths.EnemyLootContainer, position, Quaternion.identity);

    public void CreateEnemySpawner(Vector3 position, string spawnerId, EnemyTypeId enemyId) {
      GameObject enemySpawner = InstantiateByPath(AssetPaths.EnemySpawner, position, Quaternion.identity);

      enemySpawner.GetComponent<EnemySpawner>().Initialize(
        spawnerId,
        enemyId,
        this
      );
    }

    public PlayerHud CreateHud() {
      var hud = InstantiateByPath(AssetPaths.Hud).GetComponent<PlayerHud>();

      hud.Initialize(
        _sceneObjects.Hero.GetComponent<HeroHealth>(),
        _sceneObjects.Hero.GetComponent<HeroInventory>(),
        _uiFactory
      );

      return hud;
    }

    private GameObject InstantiatePrefab(GameObject prefab, Vector3 position, Quaternion rotation) {
      GameObject gameObject = Object.Instantiate(prefab, position, rotation);
      _persistentProgress.BindSceneObject(gameObject);

      return gameObject;
    }

    private GameObject InstantiateByPath(string path) {
      GameObject gameObject = _assetProvider.Instantiate(path);
      _persistentProgress.BindSceneObject(gameObject);

      return gameObject;
    }

    private GameObject InstantiateByPath(string path, Vector3 position, Quaternion rotation) {
      GameObject gameObject = _assetProvider.Instantiate(path, position, rotation);
      _persistentProgress.BindSceneObject(gameObject);

      return gameObject;
    }
  }
}
