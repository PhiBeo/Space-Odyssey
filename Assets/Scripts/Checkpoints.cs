using System;
using System.Collections.Generic;
using UnityEngine;
using MyBox;
using TMPro;

[Serializable]
public struct Checkpoint
{
    public string name;
    public int distance;
}

public class Checkpoints : MonoBehaviour
{
    [SerializeField] private List<Checkpoint> checkpoints;
    [SerializeField] private TextMeshProUGUI checkpointName;

    [Header("Debug Values")]
    [ReadOnly, SerializeField] private int currentCheckpoint;
    [ReadOnly, SerializeField] private float currentDistance;

    void Start()
    {
        currentCheckpoint = 0;
        currentDistance = 0;
    }

    void Update()
    {
        if(GameManager.instance.GetGameSpeed > 0)
            currentDistance += GameManager.instance.GetGameSpeed * Time.deltaTime;

        if(currentDistance >= checkpoints[currentCheckpoint].distance)
        {
            UpdateText();
            if(currentCheckpoint >= checkpoints.Count - 1)
            {
                GameManager.instance.ReachGoal();
            }
            else
                GameManager.instance.EnterCheckpoint();
        }
    }

    private void UpdateText()
    {
        checkpointName.text = checkpoints[currentCheckpoint].name;
    }

    public void NextCheckpoint()
    {
        if (currentCheckpoint >= checkpoints.Count - 1) return;

        currentCheckpoint++;
        GameManager.instance.ExitCheckpoint();
    }

    public float GetTotalDistance()
    {
        return checkpoints[checkpoints.Count - 1].distance;
    }

    public float GetCurrentDistance => currentDistance;
}
