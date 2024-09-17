using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameoverReason : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] string caughtText;
    [SerializeField] string outOfFuelText;
    [SerializeField] string noHealthText;
    void Start()
    {
        switch (GameManager.instance.GetGameoverType)
        {
            case GameoverType.caught:
                text.text = caughtText; break;
            case GameoverType.fuel:
                text.text = outOfFuelText; break;
            case GameoverType.health:
                text.text = noHealthText; break;
                default: text.text = "/"; break;
        }
    }
}
