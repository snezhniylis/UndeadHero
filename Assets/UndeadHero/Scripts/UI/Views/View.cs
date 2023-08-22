using UndeadHero.Infrastructure.Services.ViewManagement;
using UnityEngine;
using UnityEngine.UI;

namespace UndeadHero.UI.Views {
  public abstract class View : MonoBehaviour {
    [SerializeField] private Button _closeViewButton;

    protected IViewManager ViewManager { get; private set; }

    public virtual void OnInitialized() { }
    public virtual void OnOpened() { }
    public virtual void OnClosed() { }

    protected void Initialize(IViewManager viewManager) {
      ViewManager = viewManager;

      InitializeCloseButton();
    }

    private void InitializeCloseButton() {
      if (_closeViewButton != null) {
        _closeViewButton.onClick.AddListener(() => ViewManager.CloseActive());
      }
    }
  }
}
