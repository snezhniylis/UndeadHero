using UndeadHero.Infrastructure.Services.ViewManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UndeadHero.UI.Views {
  public abstract class View : MonoBehaviour {
    [SerializeField] private Button[] _closeViewButtons;

    protected IViewManager ViewManager { get; private set; }

    protected virtual void OnShow() { }
    protected virtual void OnHide() { }

    protected void Initialize(IViewManager viewManager) {
      ViewManager = viewManager;

      InitializeCloseButton();
    }

    public void Hide() {
      gameObject.SetActive(false);
      OnHide();
    }

    public void Show() {
      gameObject.SetActive(true);
      OnShow();
    }

    private void InitializeCloseButton() {
      foreach (Button button in _closeViewButtons) {
        button.onClick.AddListener(CloseView);
      }
    }

    private void CloseView() =>
      ViewManager.CloseActive();
  }
}
