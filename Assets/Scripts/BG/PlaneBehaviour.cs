using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject bg = null;
    [SerializeField] [Range(0.0f, 1.0f)] private float planeColliderSizeCoefficient = 0.0f;

    private void Start()
    {
        transform.position = new Vector3(transform.position.x, bg.transform.position.y - ScreenInfo.bgSizeMatchX.y / 2, transform.position.z);
        transform.localScale = new Vector3(ScreenInfo.screenSize.x, ScreenInfo.bgSizeMatchX.y * planeColliderSizeCoefficient, 1.0f);
    }
}
