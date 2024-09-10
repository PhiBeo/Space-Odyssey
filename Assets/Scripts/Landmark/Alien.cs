using UnityEngine;
using MyBox;
using System;
using TMPro;

public enum AlienAction
{ 
    attack,
    gift,
    nothing
}


public class Alien : MonoBehaviour
{
    [Header("Alien Values")]
    [SerializeField, Range(0f, 100f)] private float chanceOfAttack = 75f;
    [SerializeField, Range(0f, 100f)] private float chanceOfGift = 100f;
    [SerializeField, Range(0f, 100f)] private float chanceOfNothing = 10f;
    [SerializeField] private float minDamage = 5f;
    [SerializeField] private float maxDamage = 20f;
    [SerializeField] private int maxFuelGiven = 20;
    [SerializeField] private int maxToolGiven = 2;

    [Header("UI Values")]
    [SerializeField] private GameObject dialogUI;
    [SerializeField] private GameObject actionUI;
    [SerializeField] private TextMeshProUGUI actionText;

    [SerializeField] private string attackDialog;
    [SerializeField] private string spareDialog;
    [SerializeField] private string giftDialog;

    [Header("Debug Values")]
    [ReadOnly, SerializeField] private AlienAction alienAction = AlienAction.nothing;

    private Ship ship;
    private Resources resources;

    private void Start()
    {
        if(chanceOfAttack + chanceOfGift + chanceOfNothing > 100f)
        {
            Debug.Log("Alien: Percentage are not correct there will be bug");
        }

        dialogUI.SetActive(true);
        actionUI.SetActive(false);
        ship = FindAnyObjectByType<Ship>();
        resources = FindAnyObjectByType<Resources>();

        GameManager.instance.OnExitLandmark += ResetUI;
    }

    private void OnDisable()
    {
        GameManager.instance.OnExitLandmark -= ResetUI;
    }

    public void AlienActionDice()
    {
        float chance = UnityEngine.Random.Range(0f, 100f);

        if (chance >= 0f && chance <= chanceOfNothing)
            alienAction = AlienAction.nothing;
        else if (chance > chanceOfNothing && chance <= chanceOfNothing + chanceOfGift)
            alienAction = AlienAction.gift;
        else
            alienAction = AlienAction.attack;

        UpdateUI();
    }

    private string Attack() 
    {
        float damage = UnityEngine.Random.Range(minDamage, maxDamage);

        ship.shipHealthAdjust(-damage);

        return "You lose some health ";
    }

    private string Gift()
    {
        var numberOfType = System.Enum.GetValues(typeof(ItemId)).Length;
        var pickedId = UnityEngine.Random.Range(0, numberOfType);

        int amountOfItemGiven = UnityEngine.Random.Range(1, ((ItemId)pickedId == ItemId.fuel ? maxFuelGiven : maxToolGiven));

        resources.AddItem((ItemId)pickedId, amountOfItemGiven, 1);

        return "Alien give " + amountOfItemGiven.ToString() + " " + Enum.GetName(typeof(ItemId), pickedId);
    }

    private void UpdateUI()
    {
        dialogUI.SetActive(false);
        actionUI.SetActive(true);

        switch (alienAction)
        { 
            case AlienAction.nothing:
                actionText.text = spareDialog;
                break;
            case AlienAction.gift:
                actionText.text = giftDialog;
                actionText.text += "\n\n" + Gift();
                break;
            case AlienAction.attack:
                actionText.text = attackDialog;
                actionText.text += "\n\n" + Attack();
                break;
            default:
                break;
        }
    }

    public void ResetUI()
    {
        dialogUI.SetActive(true);
        actionUI.SetActive(false);
    }
}
