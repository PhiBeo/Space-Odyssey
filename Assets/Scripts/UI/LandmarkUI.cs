using UnityEngine;

public class LandmarkUI : MonoBehaviour
{
    //parent landmark UI
    [Header("Parent UI")]
    [SerializeField] private GameObject landmarkUI;

    [Header("Child UI")]
    [SerializeField] private GameObject rockBeltUI;
    [SerializeField] private GameObject spacePirateUI;
    [SerializeField] private GameObject alienUI;
    [SerializeField] private GameObject shipYardUI;
    [SerializeField] private GameObject resourcePlanetUI;

    private void Start()
    {
        landmarkUI.SetActive(false);
        rockBeltUI.SetActive(false);
        spacePirateUI.SetActive(false);
        alienUI.SetActive(false);
        shipYardUI.SetActive(false);
        resourcePlanetUI.SetActive(false);

        GameManager.instance.OnEnterLandmark += EnterLandmark;
        GameManager.instance.OnExitLandmark += ExitLandmark;
    }

    private void OnDisable()
    {
        GameManager.instance.OnEnterLandmark -= EnterLandmark;
        GameManager.instance.OnExitLandmark -= ExitLandmark;
    }

    public void EnterLandmark()
    {
        landmarkUI.SetActive(true);

        switch (GameManager.instance.GetLandmarkType)
        {
            case LandmarkType.None:
                GameManager.instance.ExitLandmark();
                break;
            case LandmarkType.RockBelt:
                rockBeltUI.SetActive(true);
                break;
            case LandmarkType.Alien:
                alienUI.SetActive(true);
                break;
            case LandmarkType.Pirate:
                spacePirateUI.SetActive(true);
                break;
            case LandmarkType.ShipYard:
                shipYardUI.SetActive(true);
                break;
            case LandmarkType.Resource:
                resourcePlanetUI.SetActive(true);
                break;
        }
    }

    public void ExitLandmark()
    {
        landmarkUI.SetActive(false);

        rockBeltUI.SetActive(false);
        alienUI.SetActive(false);
        spacePirateUI.SetActive(false);
        shipYardUI.SetActive(false);
        resourcePlanetUI.SetActive(false);
    }
}
