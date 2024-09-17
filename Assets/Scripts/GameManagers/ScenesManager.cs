using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class ScenesManager : MonoBehaviour
{
    private GameoverType gameoverType;
    private float countDown = 3f;
    private bool gameWin = false;
    private bool gameLost = false;

    private void Update()
    {
        if(GameManager.instance.GetSceneType == SceneType.Gameplay)
        {
            if (gameWin || gameLost)
                countDown -= Time.deltaTime;
            if (countDown > 0) return;

            if (gameWin)
            {
                GameManager.instance.SetSceneType(SceneType.Outro);
                SceneManager.LoadScene("Outro");
                gameWin = false;
            }
            else if (gameLost)
            {
                GameManager.instance.SetSceneType(SceneType.Gameover);
                SceneManager.LoadScene("GameOver");
                gameLost = false;
            }
            countDown = 3f;
        }
    }
    public void GameoverScene()
    {
        gameLost = true;
    }

    public void GameWinScene()
    {
        gameWin = true;
    }

    public void EnterGameplayScene()
    {
        GameManager.instance.SetSceneType(SceneType.Gameplay);
        SceneManager.LoadScene("Gameplay");
    }

    public void EnterIntroCutscene()
    {
        GameManager.instance.SetSceneType(SceneType.Intro);
        SceneManager.LoadScene("Intro");
    }

    public void EnterOutroCutscene()
    {
        GameManager.instance.SetSceneType(SceneType.Outro);
        SceneManager.LoadScene("Outro");
    }

    public void EnterGamewinScene()
    {
        GameManager.instance.SetSceneType(SceneType.Gamewin);
        SceneManager.LoadScene("GameWin");
    }

    public void EnterMainMenu()
    {
        GameManager.instance.SetSceneType(SceneType.MainMenu);
        SceneManager.LoadScene("MainMenu");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
