namespace UndeadHero.Infrastructure.Services.Random {
  internal class Randomizer : IRandomizer {
    public int Next(int min, int max) =>
      UnityEngine.Random.Range(min, max);
  }
}
