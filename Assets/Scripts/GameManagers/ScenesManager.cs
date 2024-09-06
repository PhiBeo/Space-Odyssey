using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void GameoverScene()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void GameWinScene()
    {
        SceneManager.LoadScene("GameWin");
    }
}
