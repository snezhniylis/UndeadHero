using UnityEngine;
using UndeadHero.Data;

namespace UndeadHero.Infrastructure.Services.SaveManagement {
  public class SaveManager : ISaveManager {
    public void Save(string key, object obj) =>
      PlayerPrefs.SetString(key, obj.ToJson());

    public T Load<T>(string key) {
      string value = PlayerPrefs.GetString(key);
      return value != null ? value.ToDeserialized<T>() : default;
    }
  }
}
