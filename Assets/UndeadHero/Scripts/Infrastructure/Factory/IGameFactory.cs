using System;
using UnityEngine;
using UndeadHero.Infrastructure.Services;

namespace UndeadHero.Infrastructure.Factory {
  public interface IGameFactory : IService {
    public GameObject HeroGameObject { get; }
    public event Action OnHeroCreated;
    GameObject CreateHero(GameObject spawnPoint);
    void CreateHud();
  }
}
