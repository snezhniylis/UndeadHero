namespace UndeadHero.Infrastructure.Services.Random {
  public interface IRandomizer : IService {
    public int Next(int min, int max);
  }
}
