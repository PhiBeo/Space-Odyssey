using System;
using System.Collections;
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
    public int id;
}

public enum ItemId
{
    fuel,
    part1,
    part2,
    part3
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

    void SpawnItem(ItemData data)
    {
        GameObject newItem = Instantiate(itemPrefab, shopObject);
        ItemPurchase item = newItem.GetComponent<ItemPurchase>();
        item.SetData(data);
        item.OnItemPurchase += UpdateUI;
        var button = newItem.GetComponentsInChildren<Button>();
        button[0].onClick.AddListener(item.DecreaseQuantity);
        button[1].onClick.AddListener(item.IncreaseQuantity);
        button[2].onClick.AddListener(item.BuyItem);
    }

    void UpdateUI()
    {
        moneyText.text = resources.GetMoney.ToString();
    }
}
