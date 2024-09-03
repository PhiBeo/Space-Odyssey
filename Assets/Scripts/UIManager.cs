using DG.Tweening;
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
    [SerializeField] private GameObject landmarkUI;
    private Tween fadeTween;
    private void Start()
    {
        gameplayUI.SetActive(true);
        checkpointUI.SetActive(false);
        shopUI.SetActive(false);
        popupUI.SetActive(false);
        popupMoneyUI.SetActive(false);
        popupItemUI.SetActive(false);

        GameManager.instance.OnEnterCheckpoint += EnterCheckpoint;
        GameManager.instance.OnExitCheckpoint += ExitCheckpoint;
        GameManager.instance.OnEnterLandmark += EnterLandmark;
        GameManager.instance.OnExitLandmark += ExitLandmark;
        GameManager.instance.OnReachGoal += ReachGoal;
        GameManager.instance.OnNotEnoughItem += NotEnoughItem;
        GameManager.instance.OnNotEnoughMoney += NotEnoughMoney;
    }

    private void OnDisable()
    {
        GameManager.instance.OnEnterCheckpoint -= EnterCheckpoint;
        GameManager.instance.OnExitCheckpoint -= ExitCheckpoint;
        GameManager.instance.OnReachGoal -= ReachGoal;
        GameManager.instance.OnNotEnoughItem -= NotEnoughItem;
        GameManager.instance.OnNotEnoughMoney -= NotEnoughMoney;
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

    void EnterLandmark()
    {

    }

    void ExitLandmark()
    {

    }

    void ReachGoal()
    {
        gameplayUI.SetActive(false);
        checkpointUI.SetActive(true);
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
}
