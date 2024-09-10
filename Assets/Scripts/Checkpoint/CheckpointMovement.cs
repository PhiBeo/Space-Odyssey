using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMovement : MonoBehaviour
{
    void Update()
    {
        if (!GameManager.instance.IsRunning) return;

        transform.Translate(Vector3.left * GameManager.instance.GetGameSpeed * Time.deltaTime);
    }

    public void SetPosition(float xPostition)
    {
        transform.position = new Vector2(xPostition, 0);
    }
}
