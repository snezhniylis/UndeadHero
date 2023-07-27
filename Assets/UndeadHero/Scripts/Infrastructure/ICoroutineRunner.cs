using System.Collections;
using UnityEngine;

namespace UndeadHero.Infrastructure {
  public interface ICoroutineRunner {
    Coroutine StartCoroutine(IEnumerator coroutine);
  }
}
