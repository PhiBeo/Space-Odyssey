using MyBox;
using System;
using UnityEngine;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance = null;

    //Gameplay variables
    [Header("Spaceship Speed")]
    [SerializeField] private float slowPace = 0.5f;
    [SerializeField] private float defaultPace = 1.0f;
    [SerializeField] private float fastPace = 2.0f;

    //Logical Variables
    [Header("Debug Values")]
    [ReadOnly, SerializeField]private float currentGamePace;
    [ReadOnly, SerializeField]private Speed speed;
    [ReadOnly, SerializeField] private LandmarkType currentLandmarkType = LandmarkType.None;
    private bool lastCheckpoint = false;

    //Events
    public Action OnPaceChange;
    public Action OnEnterCheckpoint;
    public Action OnExitCheckpoint;
    public Action OnEnterLandmark;
    public Action OnExitLandmark;
    public Action OnReachGoal;
    public Action OnNotEnoughItem;
    public Action OnNotEnoughMoney;
    public Action OnTakeOff;
    public Action OnSpeedChange;
    public Action OnGameOver;
    public Action OnPauseGame;
    public Action OnUnpauseGame;

    //Landmark Events
    public Action OnEnterRockBelt;
    public Action OnMeetingPirate;
    public Action OnMeetingAlien;
    public Action OnEnterShipYard;
    public Action OnEnterResourcePlanet;

    private ScenesManager sceneManager;

    private bool isRunning;

    void Awake()
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
        currentGamePace = 0;
        speed = Speed.stop;
        lastCheckpoint = false;
        isRunning = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            PauseGame();
        }
    }

    public void Gameover(GameoverType type)
    {
        currentGamePace = 0;
        speed = Speed.stop;
        OnGameOver?.Invoke();
        GameManager.instance.Gameover(type);
    }

    public void EnterCheckpoint()
    {
        currentGamePace = 0;
        speed = Speed.stop;
        OnEnterCheckpoint?.Invoke();
    }

    public void ExitCheckpoint()
    {
        currentGamePace = defaultPace;
        speed = Speed.normal;
        OnExitCheckpoint?.Invoke();
        UpdateSpeedUI();
    }

    public void EnterLandmark(LandmarkType type)
    {
        currentGamePace = 0;
        speed = Speed.stop;
        currentLandmarkType = type;
        OnEnterLandmark?.Invoke();
    }

    public void ExitLandmark()
    {
        currentGamePace = defaultPace;
        speed = Speed.normal;
        OnExitLandmark?.Invoke();
        currentLandmarkType = LandmarkType.None;
        UpdateSpeedUI();
    }

    public void ReachGoal()
    {
        currentGamePace = 0;
        speed = Speed.stop;
        lastCheckpoint = true;
        OnReachGoal?.Invoke();
        GameManager.instance.ReachGoal();
    }

    public void PauseGame()
    {
        currentGamePace = 0;
        speed = Speed.stop;
        OnPauseGame?.Invoke();
    }

    public void UnpauseGame()
    {
        currentGamePace = defaultPace;
        speed = Speed.normal;
        OnUnpauseGame?.Invoke();
    }

    public void NotEnoughMoney()
    {
        OnNotEnoughMoney?.Invoke();
    }

    public void NotEnoughItem()
    {
        OnNotEnoughItem?.Invoke();
    }

    public void SetSlowSpeed()
    {
        speed = Speed.slow;
        currentGamePace = slowPace;
        UpdateSpeedUI();
    }

    public void SetFastSpeed()
    {
        speed = Speed.fast;
        currentGamePace = fastPace;
        UpdateSpeedUI();
    }

    public void SetNormalSpeed()
    {
        speed = Speed.normal;
        currentGamePace = defaultPace;
        UpdateSpeedUI();
    }

    public void ShipSetting()
    {
        speed = Speed.stop;
        currentGamePace = 0;
    }

    public void TakeOff()
    {
        speed = Speed.normal;
        currentGamePace = defaultPace;
        isRunning = true;
        OnTakeOff?.Invoke();
        UpdateSpeedUI();
    }

    public void UpdateSpeedUI()
    {
        OnSpeedChange?.Invoke();
    }

    public void EnableGameObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }

    public float GetGameSpeed => currentGamePace;
    public bool IsFinish { get => lastCheckpoint; }
    public Speed Speed { get => speed; }
    public LandmarkType GetLandmarkType => currentLandmarkType;
    public bool IsRunning => isRunning;
}