using System.Collections.Generic;
using TMPro;
using UndeadHero.Character.Hero;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.UI.Elements;
using UnityEngine;

namespace UndeadHero.UI.Views {
  public class HalloweenShopView : View {
    [SerializeField] private TMP_Text _essenceCounter;
    [SerializeField] private List<ShopItemCard> _itemCards;

    private HeroInventory _heroInventory;

    public void Initialize(IViewManager viewManager, HeroInventory heroInventory) {
      base.Initialize(viewManager);

      _heroInventory = heroInventory;

      InitializeItemCards();
    }

    private void InitializeItemCards() {
      foreach (ShopItemCard itemCard in _itemCards) {
        itemCard.OnBuyButtonPressed += PurchaseItem;
      }
    }

    protected override void OnShow() {
      _heroInventory.OnEssenceAmountChanged += UpdateEssenceRelatedThings;
      UpdateEssenceRelatedThings(_heroInventory.Essence);
    }

    protected override void OnHide() =>
      _heroInventory.OnEssenceAmountChanged -= UpdateEssenceRelatedThings;

    private void PurchaseItem(int itemPrice) =>
      _heroInventory.WithdrawEssence(itemPrice);

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
