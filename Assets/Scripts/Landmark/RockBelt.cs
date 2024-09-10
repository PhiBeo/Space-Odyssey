using UnityEngine;
using MyBox;
using TMPro;

public class RockBelt : MonoBehaviour
{
    [Header("Adjust Values")]
    [SerializeField] private int mindaySkipped = 3;
    [SerializeField] private int maxdaySkipped = 5;
    [SerializeField] private float minPercentHealthLost = 20f;
    [SerializeField] private float maxPercentHealthLost = 50f;

    [Header("UI Values")]
    [SerializeField] private GameObject actionTakenUI;
    [SerializeField] private TextMeshProUGUI actionTakenText;

    [Header("Debug Values")]
    [ReadOnly, SerializeField] private int dayLost = 0;
    [ReadOnly, SerializeField] private float healthLost = 0f;

    private IncrementCalenderTime calender;
    private Ship ship;
    private Police police;

    private void Start()
    {
        calender = FindAnyObjectByType<IncrementCalenderTime>();
        ship = FindAnyObjectByType<Ship>();
        police = FindAnyObjectByType<Police>();
        actionTakenUI.SetActive(false);

        GameManager.instance.OnExitLandmark += ResetUI;
    }

    private void OnDisable()
    {
        GameManager.instance.OnExitLandmark -= ResetUI;
    }

    public void GoAroundRockBelt()
    {
        dayLost = Random.Range(mindaySkipped, maxdaySkipped);
        
        calender.TimeProcess(dayLost);
        police.SkipTheDay(dayLost);

        UpdateUI(true);
    }

    public void GoThroughRockBelt()
    {
        var randomPercent = Random.Range(minPercentHealthLost, maxPercentHealthLost);
        healthLost = (ship.GetHealth * randomPercent) / 100;

        ship.shipHealthAdjust(-healthLost);

        UpdateUI(false);
    }

    public void UpdateUI(bool goAround)
    {
        actionTakenUI.SetActive(true);

        if (goAround) 
        {
            actionTakenText.text = $"You go around the rock belt. It takes you {dayLost} days.";
        }
        else
        {
            actionTakenText.text = $"You go through the rock belt and lose some health";
        }
    }

    public void ResetUI()
    {
        actionTakenUI.SetActive(false);
    }
}
