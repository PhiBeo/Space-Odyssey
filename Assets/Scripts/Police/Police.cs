using UnityEngine;
using MyBox;

public class Police : MonoBehaviour
{
    [Header("Default Setting")]
    [SerializeField] private float distanceFromShip = 100f;
    [SerializeField, Range(0.1f, 3f)] private float speed = 1.5f;

    [Header("Debug values")]
    [ReadOnly, SerializeField] private float currentDistance;

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
        if (!GameManager.instance.IsRunning) return;
        if (GameManager.instance.Speed == Speed.stop) return;

        currentDistance += speed * Time.deltaTime;

        float desiredSpeed = speed - GameManager.instance.GetGameSpeed;

        transform.Translate(Vector2.right * desiredSpeed * Time.deltaTime);  
    }

    private void FixedUpdate()
    {
        if (transform.position.x > ship.transform.position.x)
            GameManager.instance.Gameover(GameoverType.caught);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player") 
        {
            GameManager.instance.Gameover(GameoverType.caught);
        }
    }

    public void SkipTheDay(int day)
    {
        transform.Translate(Vector3.right * speed * (day * calenderTime.GetRate), Space.World);
    }

    public float GetCurrentDistance => currentDistance;
}
