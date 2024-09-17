using System;
using TMPro;
using UnityEngine;

public class ItemPurchase : MonoBehaviour
{
    private ItemData itemData;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI quantity;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI amountPerPurchase;
    [SerializeField] private TextMeshProUGUI totalOfPurchase;

    public Action OnItemPurchase;
    public Action OnNotEnoughMoney;
    public Action OnNotEnoughItem;

    private Resources resources;

    private void Start()
    {
        resources = FindAnyObjectByType<Resources>();
    }

    public void SetData(ItemData data)
    {
        itemData = data;
        UpdateUI();
    }

    public void IncreaseQuantity()
    {
        itemData.quantity++;
        UpdateUI();
    }
    public void DecreaseQuantity() 
    {
        if (itemData.quantity <= 0) return;
        itemData.quantity--;
        UpdateUI();
    }
    public void BuyItem()
    {
        if (resources.GetMoney <= 0 || itemData.quantity * itemData.price > resources.GetMoney)
        {
            GameplayManager.instance.NotEnoughMoney();
            return;
        }

        resources.AddItem((ItemId)itemData.id, itemData.quantity, itemData.perPurchase);
        resources.MoneyCalculation(-(itemData.quantity * itemData.price));

        UpdateUI();
    }

    public void SellItem()
    {
        if (itemData.quantity * itemData.perPurchase > resources.CheckItemAmount(itemData.id))
        {
            GameplayManager.instance.NotEnoughItem();
            return;
        }

        resources.RemoveItem((ItemId)itemData.id, itemData.quantity, itemData.perPurchase);
        resources.MoneyCalculation(itemData.quantity * itemData.price);
    }
    public void UpdateUI()
    {
        itemName.text = itemData.name;
        quantity.text = itemData.quantity.ToString();
        price.text = itemData.price.ToString();
        amountPerPurchase.text = itemData.perPurchase.ToString();
        totalOfPurchase.text = (itemData.quantity * itemData.price).ToString();
        OnItemPurchase?.Invoke();
    }
}
