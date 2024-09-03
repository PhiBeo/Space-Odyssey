using UnityEngine;
using MyBox;

public class LandmarkLogic : MonoBehaviour
{
    [ReadOnly, SerializeField] private LandmarkData landmarkData;
    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = landmarkData.landmarkDatas.mapIcon;
    }

    private void Update()
    {
        transform.Translate(Vector2.left * GameManager.instance.GetGameSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameManager.instance.EnterLandmark(landmarkData.landmarkDatas.type);
            var box = GetComponent<BoxCollider2D>();
            Destroy(box);
        }
    }

    public void SetData(LandmarkData data)
    {
        landmarkData = data;
    }


}
