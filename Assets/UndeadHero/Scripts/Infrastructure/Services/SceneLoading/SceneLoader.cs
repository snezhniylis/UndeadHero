using System;
using System.Collections;
using UndeadHero.Infrastructure.Services.Coroutines;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UndeadHero.Infrastructure.Services.SceneLoading {
  public class SceneLoader : ISceneLoader {
    private readonly ICoroutineRunner _coroutineRunner;

    public SceneLoader(ICoroutineRunner coroutineRunner) {
      _coroutineRunner = coroutineRunner;
    }

    public void Load(string name, Action onLoaded = null) =>
      _coroutineRunner.StartCoroutine(LoadScene(name, onLoaded));

    private static IEnumerator LoadScene(string name, Action onLoaded) {
      if (SceneManager.GetActiveScene().name != name) {
        AsyncOperation sceneLoading = SceneManager.LoadSceneAsync(name);
        while (!sceneLoading.isDone) {
          yield return null;
        }
      }

      onLoaded?.Invoke();
    }
  }
}
