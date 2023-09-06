using System.Collections.Generic;
using TMPro;
using UndeadHero.Character.Hero;
using UndeadHero.Infrastructure.Services.SceneObjectsRegistry;
using UndeadHero.Infrastructure.Services.ViewManagement;
using UndeadHero.UI.Elements;
using UnityEngine;
using VContainer;

namespace UndeadHero.UI.Views {
  public class HalloweenShopView : View {
    [SerializeField] private TMP_Text _essenceCounter;
    [SerializeField] private List<ShopItemCard> _itemCards;

    private HeroInventory _heroInventory;

    [Inject]
    public void Initialize(IViewManager viewManager, ISceneObjectsRegistry sceneObjects) {
      base.Initialize(viewManager);

      _heroInventory = sceneObjects.Hero.GetComponent<HeroInventory>();

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
