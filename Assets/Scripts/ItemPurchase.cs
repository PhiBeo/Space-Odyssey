using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemPurchase : MonoBehaviour
{
    private ItemData itemData;

    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI quantity;
    [SerializeField] private TextMeshProUGUI price;
    [SerializeField] private TextMeshProUGUI amountPerPurchase;

    public Action OnItemPurchase;

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
        if (resources.GetMoney <= 0) return;
        if(itemData.quantity * itemData.price > resources.GetMoney) return;

        resources.AddItem((ItemId)itemData.id, itemData.quantity);
        resources.MoneyCalculation(-(itemData.quantity * itemData.price));

        UpdateUI();
    }
    public void UpdateUI()
    {
        itemName.text = itemData.name;
        quantity.text = itemData.quantity.ToString();
        price.text = (itemData.quantity * itemData.price).ToString();
        amountPerPurchase.text = itemData.perPurchase.ToString();
        OnItemPurchase?.Invoke();
    }
}
