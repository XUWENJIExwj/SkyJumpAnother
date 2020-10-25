using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float highestPosY;

    public void Init()
    {
        highestPosY = transform.position.y;
    }

    public void UpdateCamera(float player_pos_y)
    {
        if (highestPosY < player_pos_y)
        {
            highestPosY = player_pos_y;
        }

        transform.position = new Vector3(0.0f, highestPosY, transform.position.z);
    }
}
