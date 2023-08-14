using UndeadHero.Character.Hero;
using UnityEngine;

namespace UndeadHero.Level.Pickups {
  public class ItemContainer : MonoBehaviour {
    [SerializeField] private PickUpTrigger _pickUpTrigger;

    private int _essence;

    private void Start() {
      _pickUpTrigger.OnHeroPickUp += (heroCollider) => { TransferItems(heroCollider.GetComponent<HeroInventory>()); };
    }

    public void StoreEssence(int amount) =>
      _essence = amount;

    private void TransferItems(HeroInventory heroInventory) =>
      heroInventory.AddEssence(_essence);
  }
}
