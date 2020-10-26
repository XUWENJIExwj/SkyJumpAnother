using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationMovement : MonoBehaviour
{
    [SerializeField] private float direction = 0.0f;
    [SerializeField] private float speedMin = 0.01f;
    [SerializeField] private float speedMax = 0.02f;
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private SpriteRenderer spriteRenderer = null;
    [SerializeField] private ObjectProperty objectProperty = null;

    private void Awake()
    {
        direction = (int)Mathf.Pow(-1, Random.Range(0, 99) % 2);
        speed = Random.Range(speedMin, speedMax) * ScreenInfo.screenCoefficient.x;

        spriteRenderer = GetComponent<SpriteRenderer>();
        objectProperty = GetComponent<ObjectProperty>();
    }

    private void FixedUpdate()
    {
        Movement();

        if (objectProperty.GetObjBorder().right < -ScreenInfo.screenHalfSize.x && direction < 0)
        {
            transform.position = new Vector3(ScreenInfo.screenHalfSize.x + objectProperty.GetObjHalfSize().x, transform.position.y, transform.position.z);
        }

        if (objectProperty.GetObjBorder().left > ScreenInfo.screenHalfSize.x && direction > 0)
        {
            transform.position = new Vector3(-ScreenInfo.screenHalfSize.x - objectProperty.GetObjHalfSize().x, transform.position.y, transform.position.z);
        }
    }

    private void Movement()
    {
        transform.position = new Vector3(transform.position.x + direction * speed, transform.position.y, transform.position.z);
    }
}
