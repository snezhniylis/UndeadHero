using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UndeadHero.UI.Elements {
  // Hacky unfinished prototype.
  public class ShopItemCard : MonoBehaviour {
    [SerializeField] private Button _buyButton;
    [SerializeField] private TMP_Text _priceTag;

    private int _itemPrice;

    public Action<int> OnBuyButtonPressed;

    private void Awake() {
      _buyButton.onClick.AddListener(() => OnBuyButtonPressed.Invoke(_itemPrice));
      _itemPrice = int.Parse(_priceTag.text);
    }

    public void OnHeroEssenceChanged(int newAmount) =>
      _buyButton.interactable = _itemPrice <= newAmount;
  }
}
