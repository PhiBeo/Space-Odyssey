using System.Collections.Generic;
using UnityEngine;
using MyBox;
using System;
using Unity.VisualScripting;

[Serializable]
public struct LandmarkData
{
    public Lanmark landmarkDatas;
    public float position;
}

public class LandmarkSpawner : MonoBehaviour
{
    [Header("Spawning Values")]
    [SerializeField] private int minNumberOfLandmark = 2;
    [SerializeField] private int maxNumberOfLandmark = 8;

    [Header("Landmarks")]
    [SerializeField] private List<Lanmark> landmarksSciptableObj;
    [SerializeField] private GameObject landmarkPrefab;
    [SerializeField] private Transform landmarkParent;

    [Header("Debug Values")]
    [ReadOnly, SerializeField] private int numberOfLandmark;
    [ReadOnly, SerializeField] private List<LandmarkData> landmarks;

    void Start()
    {
        landmarks = new List<LandmarkData>();
        RandomizeSpawnNumber();
        SpawnerDistribute();
        SpawnlandmarkToGameWorld();
    }

    void RandomizeSpawnNumber()
    {
        System.Random random = new System.Random();
        numberOfLandmark = random.Next(minNumberOfLandmark, maxNumberOfLandmark);
    }

    void SpawnerDistribute()
    {
        //distance
        float maxDistance = FindAnyObjectByType<Checkpoints>().GetTotalDistance();
        float lastSpawnPosition = 0f;
        float maxDistanceBetweenLandmark = maxDistance / numberOfLandmark;

        for (int i = 0; i < numberOfLandmark; i++)
        {
            float position = UnityEngine.Random.Range(lastSpawnPosition + 10.0f, (i + 1) * maxDistanceBetweenLandmark);

            LandmarkData newLandmark = new LandmarkData();
            newLandmark.position = position;
            int randomizer = UnityEngine.Random.Range(0, landmarksSciptableObj.Count);
            newLandmark.landmarkDatas = landmarksSciptableObj[randomizer];
            landmarks.Add(newLandmark);

            lastSpawnPosition += maxDistanceBetweenLandmark;
        }
    }

    void SpawnlandmarkToGameWorld()
    {
        for(int i = 0; i < numberOfLandmark; i++)
        {
            GameObject newLandmark = Instantiate(landmarkPrefab, new Vector3(landmarks[i].position, 0,0), Quaternion.identity, landmarkParent);
            newLandmark.GetComponent<LandmarkLogic>().SetData(landmarks[i]);
        }
    }
}