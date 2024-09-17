using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointMovement : MonoBehaviour
{
    void Update()
    {
        if (!GameplayManager.instance.IsRunning) return;

        transform.Translate(Vector3.left * GameplayManager.instance.GetGameSpeed * Time.deltaTime);
    }

    public void SetPosition(float xPostition)
    {
        transform.position = new Vector2(xPostition, 0);
    }
}
