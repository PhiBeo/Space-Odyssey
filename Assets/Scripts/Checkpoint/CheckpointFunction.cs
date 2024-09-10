using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckpointFunction : MonoBehaviour
{
    [Header("Talking Values")]
    [SerializeField] private List<string> localDialog;
    [SerializeField] private GameObject talkPanel;
    [SerializeField] private TextMeshProUGUI talkFieldText;

    [Header("Resting Values")]
    [SerializeField] private int dayOff = 3;
    [SerializeField] private GameObject restPanel;
    [SerializeField] private TextMeshProUGUI restText;
    [SerializeField] private TextMeshProUGUI OptionRestingText;

    [Header("Stealing Values")]
    [SerializeField, Range(0f, 100f)] private float successRate = 25f;
    [SerializeField] private int maxAmountOfMoney = 100;
    [SerializeField] private int maxAmountOfTool = 2;
    [SerializeField] private int maxAmountOfFuel = 150;
    [SerializeField] private int penaltyDay = 5;
    [SerializeField] private GameObject stealPanel;
    [SerializeField] private TextMeshProUGUI stealText;

    private void Start()
    {
        talkPanel.SetActive(false);
        restPanel.SetActive(false);
        stealPanel.SetActive(false);
        OptionRestingText.text = "Resting for " + dayOff + " days";
    }

    public void TalkWithLocal()
    {
        int dialogChoice = UnityEngine.Random.Range(0, localDialog.Count);
        talkFieldText.text = "Local: " + localDialog[dialogChoice];
    }

    public void RestForAFewDay()
    {
        FindAnyObjectByType<Ship>().FullHealing();
        FindAnyObjectByType<IncrementCalenderTime>().TimeProcess(dayOff);
        FindAnyObjectByType<Police>().SkipTheDay(dayOff);
        restText.text = dayOff + " days had pass. Your ship are now fully heal.";
    }

    public void StealFromOther()
    {
        int chance = UnityEngine.Random.Range(0, 100);

        if(chance < successRate) 
        {
            stealText.text = StealSuccess();
        }
        else
        {
            FindAnyObjectByType<IncrementCalenderTime>().TimeProcess(penaltyDay);
            FindAnyObjectByType<Police>().SkipTheDay(penaltyDay);
            stealText.text = "You got caught trying to steal, you will receive " + penaltyDay + " days penalty";
        }
    }

    private string StealSuccess()
    {
        var resources = FindAnyObjectByType<Resources>();

        var numberOfType = System.Enum.GetValues(typeof(Loot)).Length;
        var pickedId = UnityEngine.Random.Range(0, numberOfType);
        int amount = 0;

        if ((Loot)pickedId == Loot.fuel)
        {
            amount = UnityEngine.Random.Range(1, maxAmountOfFuel);
            resources.AddItem(ItemId.fuel, amount, 1);
        }
        else if ((Loot)pickedId == Loot.tool)
        {
            amount = UnityEngine.Random.Range(1, maxAmountOfTool);
            resources.AddItem(ItemId.tool, amount, 1);
        }
        else if ((Loot)pickedId == Loot.cash)
        {
            amount = UnityEngine.Random.Range(1, maxAmountOfMoney);
            resources.AddMoney(amount);
        }

        return "You Steal " + amount.ToString() + " " + Enum.GetName(typeof(Loot), pickedId);
    }
}
