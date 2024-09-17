using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ResourcePlanet : MonoBehaviour
{
    [Header("Planet Values")]
    [SerializeField, Range(0f, 100f)] private float chanceOfLoot = 35f;
    [SerializeField] private int maxAmountOfFuel = 200;

    [Header("UI Values")]
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject actionUI;
    [SerializeField] private TextMeshProUGUI actionText;

    [SerializeField] private string successDig;
    [SerializeField] private string FailDig;

    private bool success = false;

    private Ship ship;
    private Resources resources;

    void Start()
    {
        dialogUI.SetActive(true);
        actionUI.SetActive(false);
        ship = FindAnyObjectByType<Ship>();
        resources = FindAnyObjectByType<Resources>();

        GameplayManager.instance.OnExitLandmark += resetUI;
    }

    private void OnDisable()
    {
        GameplayManager.instance.OnExitLandmark -= resetUI;
    } 

    private void UpdateUI()
    {
        dialogUI.SetActive(false);
        actionUI.SetActive(true);

        if(success) 
        { 
            actionText.text = successDig;
            actionText.text += "\n\n" + AmountOfFuelFound();
        }
        else
        {
            actionText.text = FailDig;
        }
    }

    public void resetUI()
    {
        dialogUI.SetActive(true);
        actionUI.SetActive(false);
    }

    public void InvestigatePlanet()
    {
        int chance = UnityEngine.Random.Range(0, 100);

        if (chance <= chanceOfLoot)
            success = true;
        else success = false;

        UpdateUI();
    }

    private string AmountOfFuelFound()
    {
        int amount = UnityEngine.Random.Range(10, maxAmountOfFuel);

        resources.AddItem(ItemId.fuel, amount, 1);

        return "Found " + amount + " fuel";
    }
}
