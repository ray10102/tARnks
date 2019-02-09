using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    [SerializeField] private float maxTurnDegree = 15;
    [SerializeField] private float maxJoystickRadius = 100;
    [SerializeField] private float deadJoystickRadiusPercent = 0.3F;
    [SerializeField] private bool rightSideOfScreen = false;
    private int fingieID = -1;
    private Vector2 fingieLocation;
    private int max_fingie_radius = 10;
    public float angle;
    public float magnitude;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        foreach (Touch touch in Input.touches)
        {
            if (fingieID == -1 && touch.phase == TouchPhase.Began &&
                ((!rightSideOfScreen && touch.position.x < Screen.width / 2) ||
                 (rightSideOfScreen && touch.position.x > Screen.width / 2)))
            {
                fingieID = touch.fingerId;
                fingieLocation = touch.position;
            }
            else if (touch.fingerId == fingieID)
            {
                if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                {
                    fingieID = -1;
                }
                else
                {
                    int direction;
                    float ang = Vector2.SignedAngle(new Vector2(0, 1), touch.position - fingieLocation);
                    ang = ang * -1;
                    if (Mathf.Abs(angle) < 90)
                    {
                        direction = 1;
                    }
                    else
                    {
                        direction = -1;
                    }
                    float radius = Vector2.Distance(fingieLocation, touch.position);
                    float percent_power = Mathf.Max(radius / maxJoystickRadius, 1);

                    if (percent_power < deadJoystickRadiusPercent)
                    {
                        magnitude = 0;
                    }
                    else
                    {
                        magnitude = direction * percent_power;
                    }
                    if (Mathf.Abs(angle) / 90 > deadJoystickRadiusPercent)
                    {
                        angle = 0;
                    }
                    else
                    {
                        angle = ang;
                    }
                }
            }
        }
    }
}