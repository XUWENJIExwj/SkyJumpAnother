using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecorationMovement : MonoBehaviour
{
    [SerializeField] private float direction;
    [SerializeField] private float speed;
    [SerializeField] private float pos_Left;
    [SerializeField] private float pos_Right;
    [SerializeField] private float halfSize;
    private float halfScreenWidth;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        direction = (int)Mathf.Pow(-1, Random.Range(0, 99) % 2);
        speed = Random.Range(0.01f, 0.03f);

        halfScreenWidth = (float)Screen.width / 100 / 2;

        spriteRenderer = GetComponent<SpriteRenderer>();
        //halfSize = spriteRenderer.size.x * transform.localScale.x / 2;
    }

    private void FixedUpdate()
    {
        Movement();

        halfSize = spriteRenderer.size.x * transform.localScale.x / 2;

        pos_Right = transform.position.x + halfSize;
        pos_Left = transform.position.x - halfSize;

        if (pos_Right <= -halfScreenWidth && direction < 0)
        {
            transform.position = new Vector3(halfScreenWidth + halfSize, transform.position.y, transform.position.z);
        }

        if (pos_Left >= halfScreenWidth && direction > 0)
        {
            transform.position = new Vector3(-halfScreenWidth - halfSize, transform.position.y, transform.position.z);
        }
    }

    // Update is called once per frame
    public void Movement()
    {
        transform.position = new Vector3(transform.position.x + direction * speed, transform.position.y, transform.position.z);
    }
}
