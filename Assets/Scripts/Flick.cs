using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flick : MonoBehaviour
{
    Camera cam;

    public ObjectWithFlick objWithFlick;
    public Trajectory trajectory;
    public bool continueJump = false;

    AudioManager audioManager;

    [SerializeField] float pushForce = 4f;

    [SerializeField] Vector2 startPoint;
    [SerializeField] Vector2 endPoint;
    Vector2 direction;
    Vector2 force;
    public float distance;
    [SerializeField] [Range(0.0f, 3.0f)] float distanceMax = 0.0f;

    bool isDragging = false;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        //objWithFlick.DisactivateRb();

        float y = ScreenInfo.screenCoefficient.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            continueJump = !continueJump;
        }

        if ((objWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE ||
            objWithFlick.playerState == ObjectWithFlick.PlayerState.PLAYER_STATE_TAP ||
            continueJump) && !objWithFlick.GetIfGameOver())
        {
            if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    OnDragStart();
                }
                else if (Input.GetMouseButtonUp(0))
                {
                    OnDragEnd();
                }
                else
                {
                    OnDrag();
                }
            }
            else
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);

                    switch (touch.phase)
                    {
                        case TouchPhase.Began:
                            OnDragStart();
                            break;
                        case TouchPhase.Moved:
                            OnDrag();
                            break;
                        case TouchPhase.Ended:
                            OnDragEnd();
                            break;
                    }
                }
            }
        }
    }

    // Drag
    void OnDragStart()
    {
        isDragging = true;
        objWithFlick.DisactivateRb(continueJump);
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        endPoint = startPoint;
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;


        objWithFlick.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_TAP);
        trajectory.UpdateDots(objWithFlick.pos, force);
        trajectory.Show();
    }

    void OnDrag()
    {
        if(isDragging)
        {
            endPoint = cam.ScreenToWorldPoint(Input.mousePosition);

            distance = Vector2.Distance(startPoint, endPoint);

            if (distance > distanceMax)
            {
                distance = distanceMax;
            }

            direction = (startPoint - endPoint).normalized;
            force = direction * distance * pushForce;

            //just for debug
            //Debug.DrawLine(startPoint, endPoint);

            objWithFlick.UpdateDirection(direction.x);

            trajectory.UpdateDots(objWithFlick.pos, force);
        }
    }

    void OnDragEnd()
    {
        if (isDragging)
        {
            //push the object
            objWithFlick.ActivateRb();

            objWithFlick.Push(force);

            objWithFlick.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_UP);

            distance = 0.0f;

            trajectory.Hide();

            isDragging = false;
        }
    }
}
