using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private CharacterController character;
    private Joystick joystick;

    [SerializeField] private float maxTurnDegree = 15;
    [SerializeField] private float moveSpeed = 2f;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();
        Joystick j1 = FindObjectOfType<Joystick>();
        Joystick j2 = FindObjectOfType<Joystick>();
        if (j1.GetInstanceID() == 0)
        {
            joystick = j2;
        }
        else
        {
            joystick = j1;
        }
    }

    void Update()
    {
        Vector3 moveVector = (Vector3.right * joystick.Horizontal + Vector3.forward * joystick.Vertical);

        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            transform.Translate(moveVector * moveSpeed * Time.deltaTime, Space.World);
        }
    }
}
