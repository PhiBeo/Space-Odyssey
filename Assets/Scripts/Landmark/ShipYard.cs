using System;
using TMPro;
using UnityEngine;

public enum Loot
{
    cash,
    fuel,
    tool
}

public class ShipYard : MonoBehaviour
{
    [Header("Ship Yard Values")]
    [SerializeField, Range(0f, 100f)] private float changeOfLoot = 25f;
    [SerializeField] private int maxAmountOfMoney = 200;
    [SerializeField] private int maxAmountOfTool = 2;
    [SerializeField] private int maxAmountOfFuel = 200;
    [SerializeField] private float maxAmountOfDamageCanReceive = 10f;

    [Header("UI Values")]
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject actionUI;
    [SerializeField] private TextMeshProUGUI actionText;

    private bool foundSomething;
    private float damageTaken;

    private Ship ship;
    private Resources resources;

    private void Start()
    {
        dialogUI.SetActive(true);
        actionUI.SetActive(false);
        ship = FindAnyObjectByType<Ship>();
        resources = FindAnyObjectByType<Resources>();

        GameManager.instance.OnExitLandmark += resetUI;
    }

    private void OnDisable()
    {
        GameManager.instance.OnExitLandmark -= resetUI;
    }

    public void LootTheSpaceShip()
    {
        float chance = UnityEngine.Random.Range(0f, 101f);
        if(chance <= changeOfLoot) 
            foundSomething = true;
        else
            foundSomething = false;

        damageTaken = UnityEngine.Random.Range(1f, maxAmountOfDamageCanReceive);
        ship.shipHealthAdjust(-damageTaken);
        damageTaken = Mathf.RoundToInt(damageTaken);

        UpdateUI();
    }

    private string LootFounded()
    {
        var numberOfType = System.Enum.GetValues(typeof(Loot)).Length;
        var pickedId = UnityEngine.Random.Range(0, numberOfType);
        int amount = 0;

        if((Loot)pickedId == Loot.fuel)
        {
            amount = UnityEngine.Random.Range(1, maxAmountOfFuel);
            resources.AddItem(ItemId.fuel, amount, 1);
        }
        else if((Loot)pickedId == Loot.tool)
        {
            amount = UnityEngine.Random.Range(1, maxAmountOfTool);
            resources.AddItem(ItemId.tool, amount, 1);
        }
        else if((Loot)pickedId == Loot.cash)
        {
            amount = UnityEngine.Random.Range(1, maxAmountOfMoney);
            resources.AddMoney(amount);
        }

        return "You Found " + amount.ToString() + " " + Enum.GetName(typeof(Loot), pickedId);
    }

    private void UpdateUI()
    {
        dialogUI.SetActive(false); 
        actionUI.SetActive(true);

        if(foundSomething)
        {
            actionText.text = LootFounded() + $"\n\nDamage Taken: {damageTaken}";
        }
        else
        {
            actionText.text = $"You found nothing\n\nDamage Taken: {damageTaken}";
        }
    }

    public void resetUI()
    {
        dialogUI.SetActive(true);
        actionUI.SetActive(false);
    }
}
