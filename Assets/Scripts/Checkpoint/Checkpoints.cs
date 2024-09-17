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
    public Sprite sprite;
}

public class Checkpoints : MonoBehaviour
{
    [Header("Checkpoints Values")]
    [SerializeField] private List<Checkpoint> checkpoints;
    [SerializeField] private TextMeshProUGUI checkpointName;

    [SerializeField] private GameObject checkPointPrefab;
    [SerializeField] private Transform checkPointParent;

    [Header("UI Values")]
    [SerializeField] private TextMeshProUGUI nextCheckpointDistanceText;
    [SerializeField] private TextMeshProUGUI currentDistanceText;
    [SerializeField] private TextMeshProUGUI disanceFromPoliceText;
    
    [Header("Debug Values")]
    [ReadOnly, SerializeField] private int nextCheckpoint;
    [ReadOnly, SerializeField] private int daysOfStaying = 1;

    private Ship ship;
    void Start()
    {
        ship = FindAnyObjectByType<Ship>();
        nextCheckpoint = -1;
        SpawnCheckpoint();
    }

    void Update()
    {
        if (!GameManager.instance.IsRunning) return;

        if (ship.GetCurrentDistance >= checkpoints[nextCheckpoint].distance)
        {
            UpdateText();
            if(nextCheckpoint >= checkpoints.Count - 1)
            {
                GameManager.instance.ReachGoal();
            }
            else
            {
                GameManager.instance.EnterCheckpoint();
            }
        }

        UpdateUI();
    }

    private void UpdateText()
    {
        checkpointName.text = checkpoints[nextCheckpoint].name;
    }

    private void SpawnCheckpoint()
    {
        foreach(var checkpoint in checkpoints)
        {
            var spawn = Instantiate(checkPointPrefab, checkPointParent);
            spawn.GetComponent<CheckpointMovement>().SetPosition(checkpoint.distance);
            spawn.GetComponent<SpriteRenderer>().sprite = checkpoint.sprite;
        }
    }

    private void UpdateUI()
    {
        nextCheckpointDistanceText.text = Mathf.RoundToInt(GetNextCheckpointDistance()).ToString() + " Units";
        currentDistanceText.text = Mathf.RoundToInt(ship.GetCurrentDistance).ToString() + " Units";
        disanceFromPoliceText.text = Mathf.RoundToInt(ship.transform.position.x - FindAnyObjectByType<Police>().transform.position.x).ToString() + " Units";
    }

    public void NextCheckpoint()
    {
        if (nextCheckpoint >= checkpoints.Count - 1) return;

        nextCheckpoint++;
        GameManager.instance.ExitCheckpoint();

        FindAnyObjectByType<Police>().SkipTheDay(daysOfStaying);

        if (GameManager.instance.IsRunning) return;

        GameManager.instance.TakeOff();
    }

    public float GetTotalDistance()
    {
        return checkpoints[checkpoints.Count - 1].distance;
    }

    public float GetNextCheckpointDistance()
    {
        return checkpoints[nextCheckpoint].distance - ship.GetCurrentDistance;
    }

    public List<float> GetCheckpointsPosition()
    {
        List<float> positionList = new List<float>();

        foreach (var checkpoint in checkpoints) 
        {
            positionList.Add(checkpoint.distance);
        }

        return positionList;
    }

    public float[] GetCheckpointPosition()
    {
        float[] distances = new float[checkpoints.Count];

        for (int i = 0; i < checkpoints.Count; i++)
        {
            distances[i] = checkpoints[i].distance;
        }

        return distances;
    }
}
