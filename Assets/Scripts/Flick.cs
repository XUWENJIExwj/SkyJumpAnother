using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flick : MonoBehaviour
{
    [SerializeField] private Camera cam = null;
    [SerializeField] private ObjectWithFlick player = null;
    [SerializeField] private Trajectory trajectory = null;

    [SerializeField] private bool unlimitedJump = false;
    [SerializeField] float pushForce = 4.0f;
    [SerializeField] private Vector2 startPoint = Vector2.zero;
    [SerializeField] private Vector2 endPoint = Vector2.zero;
    [SerializeField] private Vector2 direction = Vector2.zero;
    [SerializeField] private Vector2 force = Vector2.zero;
    [SerializeField] private float distance = 0.0f;
    [SerializeField] [Range(0.0f, 3.0f)] private float distanceMax = 0.0f;

    [SerializeField] private bool isDragging = false;

    // Start is called before the first frame update
    private void Awake()
    {
        //objWithFlick.DisactivateRb();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            unlimitedJump = !unlimitedJump;
        }

        if ((player.GetPlayerState() == ObjectWithFlick.PlayerState.PLAYER_STATE_IDLE ||
            player.GetPlayerState() == ObjectWithFlick.PlayerState.PLAYER_STATE_TAP ||
            unlimitedJump) && !player.GetIfGameOver())
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
    private void OnDragStart()
    {
        isDragging = true;
        player.DisactivateRb(unlimitedJump);
        startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
        endPoint = startPoint;
        distance = Vector2.Distance(startPoint, endPoint);
        direction = (startPoint - endPoint).normalized;
        force = direction * distance * pushForce;

        player.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_TAP);
        trajectory.UpdateDots(player.pos, force);
        trajectory.Show();
    }

    private void OnDrag()
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

            player.UpdateDirection(direction.x);

            trajectory.UpdateDots(player.pos, force);
        }
    }

    private void OnDragEnd()
    {
        if (isDragging)
        {
            //push the object
            player.ActivateRb();

            player.SetJump(force);

            player.SetPlayerState(ObjectWithFlick.PlayerState.PLAYER_STATE_JUMP_UP);

            distance = 0.0f;

            trajectory.Hide();

            isDragging = false;
        }
    }
}
