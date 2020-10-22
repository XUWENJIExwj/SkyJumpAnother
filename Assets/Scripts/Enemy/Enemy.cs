using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int direction;
    private float speed;
    public float speedMin;
    public float speedMax;

    private float screenWidth;
    private float halfScreenWidth;
    private float screenHeight;
    private float halfScreenHeight;

    private bool isRight;

    private SpriteAnimation enemySpriteAnimation;

    // Start is called before the first frame update
    void Start()
    {
        direction = (int)Mathf.Pow(-1, Random.Range(0, 99) % 2);
        speed = Random.Range(speedMin, speedMax);

        screenWidth = (float)Screen.width / 100;
        halfScreenWidth = screenWidth / 2;
        screenHeight = (float)Screen.height / 100;
        halfScreenHeight = screenHeight / 2;

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

        if ((direction < 0 && transform.position.x < -halfScreenWidth) || (direction > 0 && transform.position.x > halfScreenWidth))
        {
            direction *= -1;
        }
    }
}
