using System.Collections.Generic;
using UndeadHero.Infrastructure.Services.UiFactory;
using UndeadHero.UI.Views;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.ViewManagement {
  public class ViewManager : IViewManager {
    private readonly IUiFactory _uiFactory;

    private readonly Dictionary<ViewId, View> _cachedViews = new();
    private readonly Stack<View> _viewStack = new();

    private View _activeView;
    private Transform _uiRoot;

    public ViewManager(IUiFactory uiFactory) {
      _uiFactory = uiFactory;
    }

    public void Open(ViewId viewId) {
      if (!_cachedViews.TryGetValue(viewId, out View newView)) {
        newView = _uiFactory.CreateView(viewId, _uiRoot);
        _cachedViews.Add(viewId, newView);
      }

      Switch(newView);
    }

    public void CloseActive() {
      _activeView.Hide();

      UnsuspendLatestView();
    }

    public void SpawnUiRoot() =>
      _uiRoot = _uiFactory.CreateUiRoot();

    private void Switch(View newView) {
      if (_activeView != null) {
        Suspend(_activeView);
      }

      _activeView = newView;
      _activeView.Show();
    }

    private void Suspend(View view) {
      view.Hide();
      _viewStack.Push(view);
    }

    private void UnsuspendLatestView() {
      if (_viewStack.TryPop(out _activeView)) {
        _activeView.Show();
      }
    }
  }
}
