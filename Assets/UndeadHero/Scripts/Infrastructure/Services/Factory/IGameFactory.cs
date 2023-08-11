using System;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.Factory {
  public interface IGameFactory : IService {
    public GameObject HeroGameObject { get; }
    public event Action OnHeroCreated;
    GameObject CreateHero(GameObject spawnPoint);
    GameObject CreateHud();
  }
}
