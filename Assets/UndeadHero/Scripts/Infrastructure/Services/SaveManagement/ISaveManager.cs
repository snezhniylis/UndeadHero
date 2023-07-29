namespace UndeadHero.Infrastructure.Services.SaveManagement {
  public interface ISaveManager : IService {
    void Save(string key, object obj);
    public T Load<T>(string key);
  }
}
