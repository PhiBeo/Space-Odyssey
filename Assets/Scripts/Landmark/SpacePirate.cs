using UnityEngine;
using MyBox;
using TMPro;
using System;

public class SpacePirate : MonoBehaviour
{
    [Header("Prirate Values")]


    [Header("UI Values")]
    [SerializeField] private GameObject originUI;
    [SerializeField] private GameObject pirateUI;
    [SerializeField] private string getRobbedDialog;
    [SerializeField] private string getSpareDialog;
    [SerializeField] private TextMeshProUGUI dialogText;

    [Header("Debug Values")]
    [ReadOnly, SerializeField] private bool getRobbed;

    private Ship ship;
    private Resources resources;
    void Start()
    {
        originUI.SetActive(true);
        pirateUI.SetActive(false);
        ship = FindAnyObjectByType<Ship>();
        resources = FindAnyObjectByType<Resources>();
        GameManager.instance.OnExitLandmark += ResetUI;
    }

    private void OnDisable()
    {
        GameManager.instance.OnExitLandmark -= ResetUI;
    }

    private void UpdateUI()
    {
        if (getRobbed)
        {
            dialogText.text = getRobbedDialog;
            dialogText.text += "\n\n" + GetRobbed();
        }
        else
        {
            dialogText.text = getSpareDialog;
            dialogText.text += "\n\n" + Donate();
        }
    }

    private string GetRobbed()
    {
        var numberOfType = System.Enum.GetValues(typeof(ItemId)).Length;
        var pickedId = UnityEngine.Random.Range(0, numberOfType);

        if(resources.CheckItemAmount((ItemId)pickedId) < 1)
        {
            pickedId = (int)ItemId.fuel;
        }

        int amountOfItemRobbed = UnityEngine.Random.Range(1, Mathf.FloorToInt(resources.CheckItemAmount((ItemId)pickedId) / 2));

        resources.RemoveItem((ItemId)pickedId, amountOfItemRobbed, 1);

        return "They took " + amountOfItemRobbed.ToString() + " " + Enum.GetName(typeof(ItemId), pickedId);
    }

    private string Donate()
    {
        var numberOfType = System.Enum.GetValues(typeof(ItemId)).Length;
        var pickedId = UnityEngine.Random.Range(0, numberOfType);

        int amountOfItemGiven = UnityEngine.Random.Range(1, Mathf.FloorToInt(resources.CheckItemAmount((ItemId)pickedId) / 2));

        resources.AddItem((ItemId)pickedId, amountOfItemGiven, 1);

        return "They give " + amountOfItemGiven.ToString() + " " + Enum.GetName(typeof(ItemId), pickedId);
    }

    public void PirateLogic()
    {
        originUI.SetActive(false);
        pirateUI.SetActive(true);

        getRobbed = UnityEngine.Random.Range(0,2) > 0 ? true : false;

        UpdateUI();
    }

    public void ResetUI()
    {
        originUI.SetActive(true);
        pirateUI.SetActive(false);
    }
}
