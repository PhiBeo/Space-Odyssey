using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneAssign : MonoBehaviour
{
    private ScenesManager scenesManager;
    void Start()
    {
        scenesManager = FindAnyObjectByType<ScenesManager>();
    }

    public void EnterGameplayScene()
    {
        scenesManager.EnterGameplayScene();
    }

    public void EnterMainMenu()
    {
        scenesManager.EnterMainMenu();
    }

    public void EnterGameover()
    {
        scenesManager.GameoverScene();
    }

    public void EnterGameWin()
    {
        scenesManager.GameWinScene();
    }

    public void EnterIntro()
    {
        scenesManager.EnterIntroCutscene();
    }

    public void EnterOutro()
    {
        scenesManager.EnterOutroCutscene();
    }

    public void ExitGame()
    {
        scenesManager.ExitGame();
    }
}
