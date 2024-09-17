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
    [SerializeField] private GameObject pauseMenuUI;

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
        pauseMenuUI.SetActive(false);

        GameplayManager.instance.OnTakeOff += EnableInitialDisableObject;
        GameplayManager.instance.OnEnterCheckpoint += EnterCheckpoint;
        GameplayManager.instance.OnExitCheckpoint += ExitCheckpoint;
        GameplayManager.instance.OnReachGoal += ReachGoal;
        GameplayManager.instance.OnNotEnoughItem += NotEnoughItem;
        GameplayManager.instance.OnNotEnoughMoney += NotEnoughMoney;
        GameplayManager.instance.OnEnterLandmark += DisableGameplayUI;
        GameplayManager.instance.OnExitLandmark += EnableGameplayerUI;
        GameplayManager.instance.OnSpeedChange += SpeedChange;
        GameplayManager.instance.OnGameOver += GameoverUI;
        GameplayManager.instance.OnPauseGame += PauseGame;
        GameplayManager.instance.OnUnpauseGame += UnpauseGame;

        foreach(var gameObject in disableOnOpen)
        {
            gameObject.SetActive(false);
        }

        GameplayManager.instance.EnterCheckpoint();

        buttonColor = Color.magenta;
    }

    private void OnDisable()
    {
        GameplayManager.instance.OnEnterCheckpoint -= EnterCheckpoint;
        GameplayManager.instance.OnExitCheckpoint -= ExitCheckpoint;
        GameplayManager.instance.OnReachGoal -= ReachGoal;
        GameplayManager.instance.OnNotEnoughItem -= NotEnoughItem;
        GameplayManager.instance.OnNotEnoughMoney -= NotEnoughMoney;
        GameplayManager.instance.OnEnterLandmark -= DisableGameplayUI;
        GameplayManager.instance.OnExitLandmark -= EnableGameplayerUI;
        GameplayManager.instance.OnTakeOff -= EnableInitialDisableObject;
        GameplayManager.instance.OnSpeedChange -= SpeedChange;
        GameplayManager.instance.OnGameOver -= GameoverUI;
        GameplayManager.instance.OnPauseGame -= PauseGame;
        GameplayManager.instance.OnUnpauseGame -= UnpauseGame;
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

    void GameoverUI()
    {
        gameplayUI.SetActive(true);
        checkpointUI.SetActive(false);
        shopUI.SetActive(false);
        popupUI.SetActive(false);
        popupMoneyUI.SetActive(false);
        popupItemUI.SetActive(false);
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

        switch (GameplayManager.instance.Speed)
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

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
    }

    public void UnpauseGame()
    {
        pauseMenuUI.SetActive(false );
    }
}
