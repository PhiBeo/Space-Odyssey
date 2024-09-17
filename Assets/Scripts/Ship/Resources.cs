using UnityEngine;
using MyBox;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

public class Resources : MonoBehaviour
{
    [Header("Fuel Consume Multiplier")]
    [SerializeField, Range(0.1f, 3.0f)] private float slowTravel = 1f;
    [SerializeField, Range(0.1f, 3.0f)] private float normalTravel = 1f;
    [SerializeField, Range(0.1f, 3.0f)] private float fastTravel = 1.5f;

    [Header("Initial Econemy")]
    [SerializeField] private int startMoney = 1000;

    [Header("Debug Values")]
    [ReadOnly, SerializeField] private int currentFuel = 50;
    [ReadOnly, SerializeField] private int currentTool = 0;
    [ReadOnly, SerializeField] private int currentMoney = 0;

    private float floatFuel = 0;

    private void Start()
    {
        currentMoney = startMoney;
    }

    private void Update()
    {
        if (!GameplayManager.instance.IsRunning) return;

        UpdateFuel();

        if (currentFuel <= 0)
        {
            GameplayManager.instance.Gameover(GameoverType.fuel);
        }
    }

    private void UpdateFuel()
    {
        if (currentFuel <= 0) return;

        float currentMultiplier = 0;

        switch (GameplayManager.instance.Speed)
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

        floatFuel += GameplayManager.instance.GetGameSpeed * Time.deltaTime * currentMultiplier;
        if(floatFuel >= 2.0f)
        {
            currentFuel -= 2;
            floatFuel = 0f;
        }
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
    }

    public void AddItem(ItemId id, int amount, int perPurchase)
    {
        switch (id) 
        {
            case ItemId.fuel:
                currentFuel += amount * perPurchase;
                break;
            case ItemId.tool:
                currentTool += amount * perPurchase;
                break;
        }
    }

    public void RemoveItem(ItemId id, int amount, int perPurchase) 
    {
        switch (id)
        {
            case ItemId.fuel:
                currentFuel -= amount * perPurchase;
                break;
            case ItemId.tool:
                currentTool -= amount * perPurchase;
                break;
        }
    }

    public void MoneyCalculation(int amount)
    {
        currentMoney += amount;
    }

    public int CheckItemAmount(ItemId id)
    {
        switch (id)
        {
            case ItemId.fuel:
                return currentFuel;
            case ItemId.tool:
                return currentTool;
            default:
                return 0;
        }
    }

    public int GetMoney => currentMoney; 
    public float GetFuel => currentFuel;
    public int GetTool => currentTool;
}
