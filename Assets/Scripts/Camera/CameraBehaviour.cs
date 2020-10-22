using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    public GameObject player;

    float highestPosY;

    // Start is called before the first frame update
    void Start()
    {
        highestPosY = transform.position.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (highestPosY < player.transform.position.y)
        {
            highestPosY = player.transform.position.y;
        }

        transform.position = new Vector3(0.0f, highestPosY, transform.position.z);
    }
}
