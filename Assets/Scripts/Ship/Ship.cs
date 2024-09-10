using UnityEngine;
using MyBox;

public class Ship : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float healthRestore = 40f;

    [Header("Heath Drain Variable")]
    [SerializeField, Range(0f, 3f)] private float defaultSpeed = 0.1f;
    [SerializeField, Range(0f, 3f)] private float slowSpeed = 0.05f;
    [SerializeField, Range(0f, 3f)] private float fastSpeed = 1.5f;

    [Header("Ship trail")]
    [SerializeField] private Sprite[] shipTrailSprites = new Sprite[3];
    [SerializeField] private SpriteRenderer trailRender;

    [Header("Debug Values")]
    [ReadOnly, SerializeField] private float currentHealh;
    [ReadOnly, SerializeField] private bool isDamage;
    [ReadOnly, SerializeField] private float currentDistance;
    private float currentHealthDrainSpeed;
    private Resources resources;

    void Start()
    {
        resources = FindAnyObjectByType<Resources>();
        currentHealthDrainSpeed = defaultSpeed;
        currentHealh = maxHealth;
    }


    void Update()
    {
        if (!GameManager.instance.IsRunning) return;
        if (!IsAlive()) return;

        currentDistance += GameManager.instance.GetGameSpeed * Time.deltaTime;

        CheckSpeed();
        currentHealh -= currentHealthDrainSpeed * Time.deltaTime;
        if(!IsAlive())
        {
            GameManager.instance.Gameover(GameoverType.health);
        }
    }

    void CheckSpeed()
    {
        trailRender.enabled = (GameManager.instance.Speed == Speed.stop ? false : true);
        switch (GameManager.instance.Speed)
        {
            case Speed.stop:
                currentHealthDrainSpeed = 0f;
                break;
            case Speed.slow:
                currentHealthDrainSpeed = slowSpeed;
                trailRender.sprite = shipTrailSprites[0];
                break;
            case Speed.fast:
                currentHealthDrainSpeed = fastSpeed;
                trailRender.sprite = shipTrailSprites[2];
                break;
            case Speed.normal:
                currentHealthDrainSpeed = defaultSpeed;
                trailRender.sprite = shipTrailSprites[1];
                break;
        }
    }

   public void shipHealthAdjust(float health)
    {
        currentHealh += health;
    }

    public void FullHealing()
    {
        currentHealh = maxHealth;
    }

    public void PartialHealing(float amount)
    {
        currentHealh += amount;
        if (currentHealh > maxHealth) currentHealh = maxHealth;
    }

    public bool IsAlive()
    {
        return currentHealh > 0;
    }

    public void FixShip()
    {
        resources.RemoveItem(ItemId.tool, 1, 1);
        PartialHealing(healthRestore);
    }

    public float GetHealth => currentHealh;
    public float GetFuel => resources.GetFuel;
    public float GetTool => resources.GetTool;
    public float GetMaxHealth => maxHealth;
    public float GetCurrentDistance => currentDistance;
}
