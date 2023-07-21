using System.Collections;
using UnityEngine;

namespace UndeadHero.Infrastructure {
  public class LoadingScreen : MonoBehaviour {
    [SerializeField]
    private float _fadeSpeed = 1f;
    [SerializeField]
    private CanvasGroup _canvasGroup;

    void Awake() {
      DontDestroyOnLoad(this);
    }

    public void Show() {
      gameObject.SetActive(true);
      _canvasGroup.alpha = 1;
    }

    public void Hide() =>
      StartCoroutine(ScreenFadeInRoutine());

    private IEnumerator ScreenFadeInRoutine() {
      while (_canvasGroup.alpha > 0) {
        _canvasGroup.alpha -= _fadeSpeed * Time.deltaTime;
        yield return null;
      }
      gameObject.SetActive(false);
    }
  }
}