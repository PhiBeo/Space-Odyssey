using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShipUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TextMeshProUGUI fueltext;
    [SerializeField] private TextMeshProUGUI toolText;

    private Ship ship;

    private float maxHealth = 100;
    void Start()
    {
        ship = FindAnyObjectByType<Ship>();
    }

    // Update is called once per frame
    void Update()
    {
        float shipHealth = ship.GetHealth / maxHealth;
        healthSlider.value = shipHealth;

        fueltext.text = ship.GetFuel.ToString();

        toolText.text = ship.GetTool.ToString();
    }
}
