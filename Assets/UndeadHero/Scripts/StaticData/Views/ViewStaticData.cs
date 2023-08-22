using UndeadHero.UI.Views;
using UnityEngine;

namespace UndeadHero.StaticData.Views {
  [CreateAssetMenu(fileName = "ViewData", menuName = "StaticData/View")]
  public class ViewStaticData : ScriptableObject {
    public ViewId Id;
    public GameObject Prefab;
  }
}
