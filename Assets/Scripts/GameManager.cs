using MyBox;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    //Gameplay variables
    [Header("Spaceship Speed")]
    [SerializeField] private float slowPace = 0.5f;
    [SerializeField] private float defaultPace = 1.0f;
    [SerializeField] private float fastPace = 2.0f;

    //Logical Variables
    [ReadOnly, SerializeField]private float currentGamePace;
    [ReadOnly, SerializeField]private Speed speed;
    private bool lastCheckpoint = false;

    //Events
    public Action OnPaceChange;
    public Action OnEnterCheckpoint;
    public Action OnExitCheckpoint;
    public Action OnReachGoal;
    public Action OnOutOfFuel;
    public Action OnOutOfFood;
    public Action OnOutOfTool;

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
        currentGamePace = defaultPace;
        speed = Speed.normal;
        lastCheckpoint = false;
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
    }

    public void ReachGoal()
    {
        currentGamePace = 0;
        speed = Speed.stop;
        lastCheckpoint = true;
        OnReachGoal?.Invoke();
    }

    public void OutOfFuel()
    {
        currentGamePace = 0;
        speed = Speed.stop;
        OnOutOfFuel?.Invoke();
    }

    public void OutOfFood()
    {
        OnOutOfFood?.Invoke();
    }

    public void OutOfTool()
    {
        OnOutOfTool?.Invoke();
    }

    public void SetSlowSpeed()
    {
        speed = Speed.slow;
        currentGamePace = slowPace;
    }

    public void SetFastSpeed()
    {
        speed = Speed.fast;
        currentGamePace = fastPace;
    }

    public void SetNormalSpeed()
    {
        speed = Speed.normal;
        currentGamePace = defaultPace;
    }

    public float GetGameSpeed => currentGamePace;
    public bool IsFinish { get => lastCheckpoint; }
    public Speed Speed { get => speed; }
}