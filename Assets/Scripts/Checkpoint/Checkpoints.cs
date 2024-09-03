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
    [SerializeField] private List<Checkpoint> checkpoints;
    [SerializeField] private TextMeshProUGUI checkpointName;

    [SerializeField] private GameObject checkPointPrefab;
    [SerializeField] private Transform checkPointParent;
    [SerializeField] private int costOfResting = 2;
    
    [Header("Debug Values")]
    [ReadOnly, SerializeField] private int currentCheckpoint;
    [ReadOnly, SerializeField] private float currentDistance;
    [ReadOnly, SerializeField] private int daysOfStaying = 1;


    void Start()
    {
        currentCheckpoint = 0;
        currentDistance = 0;
        SpawnCheckpoint();
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

    private void SpawnCheckpoint()
    {
        foreach(var checkpoint in checkpoints)
        {
            var spawn = Instantiate(checkPointPrefab, checkPointParent);
            spawn.GetComponent<CheckpointMovement>().SetPosition(checkpoint.distance);
            spawn.GetComponent<SpriteRenderer>().sprite = checkpoint.sprite;
        }
    }

    public void NextCheckpoint()
    {
        if (currentCheckpoint >= checkpoints.Count - 1) return;

        currentCheckpoint++;
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

    public float GetCurrentDistance => currentDistance;
}
