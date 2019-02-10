using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour {
    private Joystick joystick;
    private float deadzone = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        joystick = Joystick.rightJoystick;
        // need to check that it's not shooting
        if (Mathf.Abs(joystick.Horizontal) >= deadzone)
        {
            transform.Rotate((Vector3.up * Time.deltaTime) * (joystick.Horizontal * -60));
        }
    }
}