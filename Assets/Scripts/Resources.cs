using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class Resources : MonoBehaviour
{
    [Header("Fuel Consume Multiplier")]
    [SerializeField, Range(0.1f, 3.0f)] private float slowTravel = 1f;
    [SerializeField, Range(0.1f, 3.0f)] private float normalTravel = 1f;
    [SerializeField, Range(0.1f, 3.0f)] private float fastTravel = 1.5f;

    [Header("Initial Econemy")]
    [SerializeField] private int startMoney = 1000;

    [ReadOnly, SerializeField] private int currentFuel = 1000;
    [ReadOnly, SerializeField] private int currentPart1 = 0;
    [ReadOnly, SerializeField] private int currentPart2 = 0;
    [ReadOnly, SerializeField] private int currentPart3 = 0;
    [ReadOnly, SerializeField] private int currentMoney = 0;

    private float floatFuel;

    private void Start()
    {
        currentMoney = startMoney;
        floatFuel = currentFuel;
    }

    private void Update()
    {
        UpdateFuel();

        if (currentFuel <= 0)
        {
            GameManager.instance.OutOfFuel();
        }
    }

    private void UpdateFuel()
    {
        if (currentFuel <= 0) return;

        float currentMultiplier = 0;

        switch (GameManager.instance.Speed)
        {
            case Speed.stop:
                currentMultiplier = 0f; break;
            case Speed.slow:
                currentMultiplier = slowTravel; break;
            case Speed.normal:
                currentMultiplier = normalTravel; break;
            case Speed.fast:
                currentMultiplier = fastTravel; break;  
        }

        floatFuel += GameManager.instance.GetGameSpeed * Time.deltaTime * currentMultiplier;
        if(floatFuel >= 2.0f)
        {
            currentFuel -= 2;
            floatFuel = 0f;
        }
    }

    public void AddItem(ItemId id, int amount)
    {
        switch (id) 
        {
            case ItemId.fuel:
                currentFuel += amount;
                break;
            case ItemId.part1:
                currentPart1 += amount;
                break;
            case ItemId.part2:
                currentPart2 += amount;
                break;
            case ItemId.part3:
                currentPart3 += amount;
                break;
        }
    }

    public void MoneyCalculation(int amount)
    {
        currentMoney += amount;
    }

    public int GetMoney { get => currentMoney; }
    
}
