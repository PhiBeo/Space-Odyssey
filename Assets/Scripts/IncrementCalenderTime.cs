using System;
using UnityEngine;
using MyBox;
using TMPro;
using System.Collections.Generic;

public class IncrementCalenderTime : MonoBehaviour
{
    [Header("Initial Start Date")]
    [SerializeField] private int initDay = 25;
    [SerializeField] private int initMonth = 5;
    [SerializeField] private int initYear = 2035;

    [SerializeField, Range(0.1f, 5.0f)] private float timeIncreaseRate = 1.0f;

    [SerializeField] private TextMeshProUGUI text;

    [ReadOnly, SerializeField] private int day;
    [ReadOnly, SerializeField] private int month;
    [ReadOnly, SerializeField] private int year;

    private DateTime currentTime;
    private float time;
    [ReadOnly, SerializeField] private bool isRunning = true;

    // Start is called before the first frame update
    void Start()
    {
        currentTime = new DateTime(initYear, initMonth, initDay);
        time = 0.0f;

        year = currentTime.Year;
        month = currentTime.Month;
        day = currentTime.Day;

        GameManager.instance.OnEnterCheckpoint += EnterCheckpoint;
        GameManager.instance.OnExitCheckpoint += ExitCheckpoint;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;

        time += Time.deltaTime;

        if(time >= timeIncreaseRate)
        {
            time = 0.0f;
            TimeProcess();
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
        GameManager.instance.OnEnterCheckpoint -= EnterCheckpoint;
        GameManager.instance.OnExitCheckpoint -= ExitCheckpoint;
    }

    void TimeProcess()
    {
        currentTime = currentTime.AddDays(1);

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
}
