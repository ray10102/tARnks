using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    [SerializeField] private float maxTurnDegree = 15;
    [SerializeField] private float maxJoystickRadius = 100;
    [SerializeField] private float deadJoystickRadiusPercent = 0.3F;
    private int fingieID = -1;
    private Vector2 fingieLocation;
    private int max_fingie_radius = 10;

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
                touch.position.x < Screen.width / 2) {
                fingieID = touch.fingerId;
                fingieLocation = touch.position;
            }
            else if (touch.fingerId == fingieID) {
                if (touch.phase == TouchPhase.Canceled || touch.phase == TouchPhase.Ended)
                {
                    fingieID = -1;
                }
                else
                {
                    int direction;
                    float angle = Vector2.SignedAngle(new Vector2(0, 1), touch.position - fingieLocation);
                    angle = angle * -1;
                    if (Mathf.Abs(angle) < 90)
                    {
                        direction = 1;
                    } else
                    {
                        direction = -1;
                    }
                    float magnitude = Vector2.Distance(fingieLocation, touch.position);
                    magnitude = Mathf.Max(0, magnitude - maxJoystickRadius * deadJoystickRadiusPercent);
                    if (magnitude != 0)
                    {
                        ApplyMovement(angle, direction * magnitude);
                    } 
                }
            }
        }
    }
    
    private void ApplyMovement(float angle, float force)
    {
        float ratio;
        if (angle < 90 && angle > -90) {
            ratio = angle % 90;
        } else {
            ratio = 90 - angle % 90;
        }
        // Check if in quadrant 1 or 3
        int turn_direction;
        if ((angle / 90) % 2 == 0)
        {
            turn_direction = 1;
        }
        else
        {
            turn_direction = -1;
        }
        transform.Rotate(new Vector3(0.0f, turn_direction * (90 - ratio) / 90 * maxTurnDegree), 0.0f);
        transform.localPosition += transform.forward * force/maxJoystickRadius;
    }
}
