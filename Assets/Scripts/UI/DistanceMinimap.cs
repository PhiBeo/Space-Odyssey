using MyBox;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceMinimap : MonoBehaviour
{
    [SerializeField] private Transform beginPosition;
    [SerializeField] private Transform endPosition;

    [SerializeField] private GameObject checkpointIndicatorPrefab;
    [SerializeField] private Transform checkpointParent;
    [SerializeField] private Transform shipIndicator;

    private Checkpoints checkpoints;
    private Ship ship;
    private float[] checkpointPositions;
    private float scale;
    // Start is called before the first frame update
    void Start()
    {
        checkpoints = FindAnyObjectByType<Checkpoints>();
        checkpointPositions = new float[checkpoints.GetCheckpointPosition().Length];
        checkpointPositions = checkpoints.GetCheckpointPosition();
        ship = FindAnyObjectByType<Ship>();

        shipIndicator.transform.position = beginPosition.position;

        float minimapDistance = endPosition.position.x - beginPosition.position.x;
        float realDistance = checkpoints.GetTotalDistance();

        scale = realDistance / minimapDistance;

        SpawnCheckpoint();
    }

    // Update is called once per frame
    void Update()
    {
        ShipIndicator();
    }

    void SpawnCheckpoint()
    {
        float currentDistance = 0;

        foreach (var point in checkpointPositions) 
        {
            currentDistance = point / scale;

            Instantiate(checkpointIndicatorPrefab,
                new Vector3(beginPosition.position.x + currentDistance, beginPosition.position.y, beginPosition.position.z),
                Quaternion.identity,
                checkpointParent
                );
        }
    }

    void ShipIndicator()
    {
        Vector2 begin = beginPosition.position;
        Vector2 end = endPosition.position;

        shipIndicator.transform.position = new Vector2(begin.x + ship.GetCurrentDistance / scale, begin.y);
    }
}
