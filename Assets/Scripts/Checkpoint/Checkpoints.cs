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
    [SerializeField] private int costOfResting = 2;

    [Header("UI Values")]
    [SerializeField] private TextMeshProUGUI nextCheckpointDistanceText;
    [SerializeField] private TextMeshProUGUI currentDistanceText;
    
    [Header("Debug Values")]
    [ReadOnly, SerializeField] private int nextCheckpoint;
    [ReadOnly, SerializeField] private int daysOfStaying = 1;

    private Ship ship;
    void Start()
    {
        ship = FindAnyObjectByType<Ship>();
        nextCheckpoint = 0;
        SpawnCheckpoint();
    }

    void Update()
    {
        if(ship.GetCurrentDistance >= checkpoints[nextCheckpoint].distance)
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
    }

    public void NextCheckpoint()
    {
        if (nextCheckpoint >= checkpoints.Count - 1) return;

        nextCheckpoint++;
        GameManager.instance.ExitCheckpoint();

        FindAnyObjectByType<Police>().SkipTheDay(daysOfStaying);
    }

    public float GetTotalDistance()
    {
        return checkpoints[checkpoints.Count - 1].distance;
    }

    public void Resting()
    {
        daysOfStaying += costOfResting;
        FindAnyObjectByType<Ship>().FullHealing();
    }

    public float GetNextCheckpointDistance()
    {
        return checkpoints[nextCheckpoint].distance - ship.GetCurrentDistance;
    }
}
