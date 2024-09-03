using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Police : MonoBehaviour
{
    [SerializeField] private float distanceFromShip = 100f;
    [SerializeField, Range(0.1f, 3f)] private float speed = 1f;

    private Ship ship;
    private IncrementCalenderTime calenderTime;


    void Start()
    {
        ship = FindAnyObjectByType<Ship>();
        transform.position = new Vector3(ship.transform.position.x - distanceFromShip, transform.position.y, transform.position.z);

        calenderTime = FindAnyObjectByType<IncrementCalenderTime>();
    }

    void Update()
    {
        
    }

    public void SkipTheDay(int day)
    {
        transform.Translate(Vector3.right * speed * (day * calenderTime.GetRate), Space.World);
    }
}
