using System.Collections;
using UnityEngine;

namespace UndeadHero.Infrastructure.Services.Coroutines {
  public interface ICoroutineRunner {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}
