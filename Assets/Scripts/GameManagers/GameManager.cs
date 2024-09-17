using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    [SerializeField] private SceneType sceneType;
    [ReadOnly, SerializeField] private GameoverType gameoverType;

    [SerializeField] private ScenesManager sceneManager;
    private void Awake()
    {
        // Check if instance already exists
        if (instance == null)
        {
            // If not, set instance to this
            instance = this;
        }
        // If instance already exists and it's not this, destroy this
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Sets this to not be destroyed when reloading scene
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        gameoverType = GameoverType.none;
        //sceneType = SceneType.MainMenu;
    }

    public void Gameover(GameoverType type)
    {
        gameoverType = type;
        sceneManager.GameoverScene();
    }

    public void ReachGoal()
    {
        sceneManager.GameWinScene();
    }

    public void FinishClip(bool isIntro)
    {
        if (isIntro)
        {
            sceneManager.EnterGameplayScene();
        }
        else
        {
            sceneManager.EnterGamewinScene();
        }
    }

    public void SetSceneType(SceneType type)
    {
        sceneType = type;
    }

    public SceneType GetSceneType => sceneType;
    public GameoverType GetGameoverType => gameoverType;
}
