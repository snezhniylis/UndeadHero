using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UndeadHero.Infrastructure.Services.SceneObjectsRegistry {
  public class SceneObjectsRegistry : ISceneObjectsRegistry {
    private GameObject _hero;

    public GameObject Hero {
      get => TryGet(_hero);
      set => TrySet(ref _hero, value);
    }

    private static T TryGet<T>(T value) where T : Object {
      if (value == null) {
        throw new NullReferenceException("Trying to access unassigned object.");
      }

      return value;
    }

    private static void TrySet<T>(ref T field, T value) where T : Object {
      if (field != null) {
        throw new OperationCanceledException("Trying to overwrite already registered object.");
      }

      field = value;
    }
  }
}
