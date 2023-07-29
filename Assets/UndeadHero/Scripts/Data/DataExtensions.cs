using UnityEngine;

namespace UndeadHero.Data {
  public static class DataExtensions {
    public static Vector3Data AsVectorData(this Vector3 vector) =>
      new(vector.x, vector.y, vector.z);

    public static Vector3 AsUnityVector(this Vector3Data vectorData) =>
      new(vectorData.X, vectorData.Y, vectorData.Z);
  }
}
