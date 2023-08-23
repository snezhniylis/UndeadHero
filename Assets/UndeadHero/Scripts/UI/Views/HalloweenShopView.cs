using System.Collections.Generic;
using TMPro;
using UndeadHero.UI.Elements;
using UnityEngine;

namespace UndeadHero.UI.Views {
  public class HalloweenShopView : EventView {
    [SerializeField] private TMP_Text _essenceCounter;
    [SerializeField] private List<ShopItemCard> _itemCards;

    public override void OnInitialized() {
      foreach (ShopItemCard itemCard in _itemCards) {
        itemCard.OnBuyButtonPressed += PurchaseItem;
      }
    }

    public override void OnOpened() {
      HeroInventory.OnEssenceAmountChanged += UpdateEssenceRelatedThings;
      UpdateEssenceRelatedThings(HeroInventory.Essence);
    }

    public override void OnClosed() =>
      HeroInventory.OnEssenceAmountChanged -= UpdateEssenceRelatedThings;

    private void PurchaseItem(int itemPrice) =>
      HeroInventory.WithdrawEssence(itemPrice);

    private void UpdateEssenceRelatedThings(int amount) {
      UpdateEssenceCounter(amount);
      UpdateItemCards(amount);
    }

    private void UpdateEssenceCounter(int amount) =>
      _essenceCounter.text = amount.ToString();

    private void UpdateItemCards(int amount) {
      foreach (ShopItemCard itemCard in _itemCards) {
        itemCard.OnHeroEssenceChanged(amount);
      }
    }
  }
}
