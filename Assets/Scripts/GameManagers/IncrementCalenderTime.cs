using System;
using UnityEngine;
using MyBox;
using TMPro;

public class IncrementCalenderTime : MonoBehaviour
{
    [Header("Initial Start Date")]
    [SerializeField] private int initDay = 25;
    [SerializeField] private int initMonth = 5;
    [SerializeField] private int initYear = 2035;

    [Header("Logical Values")]
    [SerializeField, Range(0.1f, 5.0f)] private float timeIncreaseRate = 1.0f;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Debug Values")]
    [ReadOnly, SerializeField] private int day;
    [ReadOnly, SerializeField] private int month;
    [ReadOnly, SerializeField] private int year;

    private DateTime currentTime;
    private float time;
    [ReadOnly, SerializeField] private bool isRunning = true;

    void Start()
    {
        currentTime = new DateTime(initYear, initMonth, initDay);
        time = 0.0f;

        year = currentTime.Year;
        month = currentTime.Month;
        day = currentTime.Day;

        GameplayManager.instance.OnEnterCheckpoint += EnterCheckpoint;
        GameplayManager.instance.OnExitCheckpoint += ExitCheckpoint;
    }

    void Update()
    {
        if (!GameplayManager.instance.IsRunning) return;
        if (!isRunning) return;

        time += Time.deltaTime;

        if(time >= timeIncreaseRate)
        {
            time = 0.0f;
            TimeProcess(1);
        }

        year = currentTime.Year;
        month = currentTime.Month;
        day = currentTime.Day;

        UpdateText();
    }

    void EnterCheckpoint()
    {
        isRunning = false;
    }

    void ExitCheckpoint()
    {
        isRunning = true;
    }

    private void OnDisable()
    {
        GameplayManager.instance.OnEnterCheckpoint -= EnterCheckpoint;
        GameplayManager.instance.OnExitCheckpoint -= ExitCheckpoint;
    }

    public void TimeProcess(int day)
    {
        currentTime = currentTime.AddDays(day);

        if (currentTime.Day == 1)
        {
            if (currentTime.Month == 1)
            {
                year++;
            }
        }
    }

    void UpdateText()
    {
        if (!text) return;

        text.text = currentTime.Day + " " + 
            GlobalData.monthNumToString[currentTime.Month] + " " + 
            currentTime.Year;
    }

    public float GetRate => timeIncreaseRate;
}
