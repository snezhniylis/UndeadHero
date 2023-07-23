namespace UndeadHero.Infrastructure.Services {
  public class GameServices {
    private static GameServices _instance;
    public static GameServices Container => _instance ??= new GameServices();

    public void RegisterSingle<TService>(TService service) where TService : IService =>
      ServiceContainer<TService>.ServiceInstance = service;

    public TService Single<TService>() where TService : IService =>
      ServiceContainer<TService>.ServiceInstance;

    private static class ServiceContainer<TService> where TService : IService {
      public static TService ServiceInstance;
    }
  }
}