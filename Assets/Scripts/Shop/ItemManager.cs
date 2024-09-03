using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public struct ItemData
{
    public string name;
    public int price;
    public int quantity;
    public int perPurchase;
    public ItemId id;
}

public enum ItemId
{
    fuel,
    tool
}

public class ItemManager : MonoBehaviour
{
    [Header("Item Spawner")]
    [SerializeField] private GameObject itemPrefab;
    [SerializeField] private Transform shopObject;
    [SerializeField] private List<ItemData> items;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI moneyText;
    public Action OnUIUpdate;

    private Resources resources;

    private void Start()
    {
        resources = FindAnyObjectByType<Resources>();

        foreach(var item in items)
        {
            SpawnItem(item);
        }

        GameManager.instance.OnEnterCheckpoint += UpdateUI;
    }

    private void Update()
    {
        
    }

    void SpawnItem(ItemData data)
    {
        GameObject newItem = Instantiate(itemPrefab, shopObject);
        ItemPurchase item = newItem.GetComponent<ItemPurchase>();
        item.SetData(data);
        item.OnItemPurchase += UpdateUI;
        item.OnNotEnoughItem += GameManager.instance.NotEnoughItem;
        item.OnNotEnoughMoney += GameManager.instance.NotEnoughMoney;
        var button = newItem.GetComponentsInChildren<Button>();
        button[0].onClick.AddListener(item.DecreaseQuantity);
        button[1].onClick.AddListener(item.IncreaseQuantity);
        button[2].onClick.AddListener(item.BuyItem);
        button[3].onClick.AddListener(item.SellItem);
    }

    void UpdateUI()
    {
        moneyText.text = "Budget: " + resources.GetMoney.ToString() + "$";
    }
}
