using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameplayUI;
    [SerializeField] private GameObject checkpointUI;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject popupUI;
    [SerializeField] private GameObject popupMoneyUI;
    [SerializeField] private GameObject popupItemUI;

    [SerializeField] private List<GameObject> disableOnOpen;

    [SerializeField] private List<Button> speedButtons;
    [SerializeField] private Color buttonColor;

    private void Start()
    {
        gameplayUI.SetActive(true);
        checkpointUI.SetActive(true);
        shopUI.SetActive(false);
        popupUI.SetActive(false);
        popupMoneyUI.SetActive(false);
        popupItemUI.SetActive(false);

        GameManager.instance.OnTakeOff += EnableInitialDisableObject;
        GameManager.instance.OnEnterCheckpoint += EnterCheckpoint;
        GameManager.instance.OnExitCheckpoint += ExitCheckpoint;
        GameManager.instance.OnReachGoal += ReachGoal;
        GameManager.instance.OnNotEnoughItem += NotEnoughItem;
        GameManager.instance.OnNotEnoughMoney += NotEnoughMoney;
        GameManager.instance.OnEnterLandmark += DisableGameplayUI;
        GameManager.instance.OnExitLandmark += EnableGameplayerUI;
        GameManager.instance.OnSpeedChange += SpeedChange;

        foreach(var gameObject in disableOnOpen)
        {
            gameObject.SetActive(false);
        }

        GameManager.instance.EnterCheckpoint();

        buttonColor = Color.magenta;
    }

    private void OnDisable()
    {
        GameManager.instance.OnEnterCheckpoint -= EnterCheckpoint;
        GameManager.instance.OnExitCheckpoint -= ExitCheckpoint;
        GameManager.instance.OnReachGoal -= ReachGoal;
        GameManager.instance.OnNotEnoughItem -= NotEnoughItem;
        GameManager.instance.OnNotEnoughMoney -= NotEnoughMoney;
        GameManager.instance.OnEnterLandmark -= DisableGameplayUI;
        GameManager.instance.OnExitLandmark -= EnableGameplayerUI;
        GameManager.instance.OnTakeOff -= EnableInitialDisableObject;
        GameManager.instance.OnSpeedChange -= SpeedChange;
    }

    void EnterCheckpoint()
    {
        gameplayUI.SetActive(false);
        checkpointUI.SetActive(true);
    }

    void ExitCheckpoint()
    {
        gameplayUI.SetActive(true);
        checkpointUI.SetActive(false);
    }

    void ReachGoal()
    {
        gameplayUI.SetActive(false);
        checkpointUI.SetActive(false);
    }

    void NotEnoughMoney()
    {
        popupUI.SetActive(true);
        popupMoneyUI.SetActive(true);
    }

    void NotEnoughItem()
    {
        popupUI.SetActive(true);
        popupItemUI.SetActive(true);
    }

    void DisableGameplayUI()
    {
        gameplayUI.SetActive(false);
    }

    void EnableGameplayerUI()
    {
        gameplayUI.SetActive(true);
    }

    public void EnableInitialDisableObject()
    {
        foreach (var gameObject in disableOnOpen)
        {
            gameObject.SetActive(true);
        }
    }

    public void SpeedChange()
    {
        for (int i = 0; i < speedButtons.Count; i++)
        {
            speedButtons[i].GetComponent<Image>().color = Color.white;
        }

        switch (GameManager.instance.Speed)
        {
            case Speed.fast:
                speedButtons[2].GetComponent<Image>().color = buttonColor;
                break;
            case Speed.normal:
                speedButtons[1].GetComponent<Image>().color = buttonColor;
                break;
            case Speed.slow:
                speedButtons[0].GetComponent<Image>().color = buttonColor;
                break;
            
        }
    }
}
