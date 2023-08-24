using UndeadHero.Infrastructure.Services.ViewManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UndeadHero.UI.Views {
  public abstract class View : MonoBehaviour {
    [SerializeField] private Button _closeViewButton;

    protected IViewManager ViewManager { get; private set; }

    public virtual void OnInitialized() { }
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
      if (_closeViewButton != null) {
        _closeViewButton.onClick.AddListener(() => ViewManager.CloseActive());
      }
    }
  }
}
