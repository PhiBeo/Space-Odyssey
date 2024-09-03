using UnityEngine;
using MyBox;

public class Ship : MonoBehaviour
{
    [Header("Heath Drain Variable")]
    [SerializeField, Range(0f, 3f)] private float defaultSpeed = 0.1f;
    [SerializeField, Range(0f, 3f)] private float slowSpeed = 0.05f;
    [SerializeField, Range(0f, 3f)] private float fastSpeed = 1.5f;

    [Header("Ship Damage Properties")]
    [Tooltip("Percentage of health that ship damage start to happen")]
    [SerializeField, Range(0f, 100f)] private float percentageStartedThreshold = 70f;
    [SerializeField, Range(0f, 100f)] private float changeOfHappen = 10f;

    [Header("Debug Values")]
    [ReadOnly, SerializeField] private float currentHealh = 100;
    [ReadOnly, SerializeField] private bool isDamage;
    private float currentHealthDrainSpeed;
    private Resources resources;

    void Start()
    {
        resources = FindAnyObjectByType<Resources>();
        currentHealthDrainSpeed = defaultSpeed;
    }


    void Update()
    {
        if (!IsAlive()) return;
        
        CheckSpeed();
        currentHealh -= currentHealthDrainSpeed * Time.deltaTime;
        if(!IsAlive())
        {
            GameManager.instance.Gameover();
        }
    }

    void CheckSpeed()
    {
        switch (GameManager.instance.Speed)
        {
            case Speed.stop:
                currentHealthDrainSpeed = 0f; break;
            case Speed.slow:
                currentHealthDrainSpeed = slowSpeed; break;
            case Speed.fast:
                currentHealthDrainSpeed = fastSpeed; break;
            case Speed.normal:
                currentHealthDrainSpeed = defaultSpeed; break;
        }
    }

    void shipDamageRandomize()
    {
        if (!IsAlive()) return;
        if (currentHealh > percentageStartedThreshold) return;

        float randomChange = Random.value * 100f;

        if (randomChange <= changeOfHappen)
        {
            isDamage = true;
        }
    }

    void shipDamageEvent()
    {
        if (!isDamage) return;

        //event that can happen when ship is damage
    }

    public bool IsAlive()
    {
        return currentHealh > 0;
    }


}
