using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private float shootAngle = Mathf.PI / 4;
    private int touchId;
    private Vector2 startPos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            for (var i = 0; i < Input.touchCount; i++)
            {
                Touch touch = Input.GetTouch(i);
                if (touch.phase == TouchPhase.Began)
                {
                    startPos = touch.position;
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    touchId = touch.fingerId;
                    float angle = Vector2.SignedAngle(new Vector2(1, 0), touch.position - startPos);
                    Debug.Log(touch.position);
                    if (angle > shootAngle)
                    {
                        Debug.Log("shot");
                    }
                    break;
                }
            }
        }
    }
}