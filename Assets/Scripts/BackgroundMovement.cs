using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    private Camera mainCamera;
    Vector2 cameraSize = Vector2.zero;
    [SerializeField] private GameObject bg1;
    [SerializeField] private GameObject bg2;

    private Vector2 spriteSize;
    void Start()
    {
        mainCamera = Camera.main;
        cameraSize.y = 2f * mainCamera.orthographicSize;
        cameraSize.x = cameraSize.y * mainCamera.aspect;

        spriteSize = bg1.GetComponent<SpriteRenderer>().bounds.size;
    }

    void Update()
    {
        bg1.transform.Translate(Vector2.left * GameManager.instance.GetGameSpeed * Time.deltaTime);
        bg2.transform.Translate(Vector2.left * GameManager.instance.GetGameSpeed * Time.deltaTime);

        float distanceToMove = (spriteSize.x / 2) + (cameraSize.x / 2);

        if (bg1.transform.position.x <= -distanceToMove)
        {
            PositionUpdate(bg1.transform, bg2.transform);
        }
        else if (bg2.transform.position.x <= -distanceToMove)
        {
            PositionUpdate(bg2.transform, bg1.transform);
        }
    }

    void PositionUpdate(Transform moveObj, Transform compareObj)
    {
        Vector2 compareObjPosition = compareObj.position;
        Vector2 newPosition = Vector2.right * (compareObjPosition + spriteSize);

        moveObj.position = newPosition;
    }
}
