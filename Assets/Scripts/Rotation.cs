using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotation : MonoBehaviour { 
    private Joystick joystick = Joystick.rightJoystick;
    private float deadzone = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //need to check that it's not shooting and that the joystick is in the right position
        if (joystick.Horizontal >= Mathf.Abs(deadzone))
        {
            transform.Rotate(Vector3.right * Time.deltaTime);
        }
    }
}
