using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int direction = 1;
    [SerializeField] private float speed = 0.0f;
    [SerializeField] private float speedMin = 0.0f;
    [SerializeField] private float speedMax = 0.0f;
    [SerializeField] private bool isRight = true;
    [SerializeField] private SpriteAnimation enemySpriteAnimation = null;

    // Start is called before the first frame update
    void Start()
    {
        direction = (int)Mathf.Pow(-1, Random.Range(0, 99) % 2);
        speed = Random.Range(speedMin, speedMax) * ScreenInfo.screenCoefficient.x;

        enemySpriteAnimation = GetComponent<SpriteAnimation>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        UpdateDirection();
        enemySpriteAnimation.UpdateEnemyAnimation();
    }

    public void UpdateDirection()
    {
        if (direction >= 0)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }

        enemySpriteAnimation.SetDirection(isRight);
    }

    private void Movement()
    {
        transform.position = new Vector3(transform.position.x + direction * speed, transform.position.y, transform.position.z);

        if ((direction < 0 && transform.position.x < -ScreenInfo.screenHalfSize.x) || (direction > 0 && transform.position.x > ScreenInfo.screenHalfSize.x))
        {
            direction *= -1;
        }
    }
}
