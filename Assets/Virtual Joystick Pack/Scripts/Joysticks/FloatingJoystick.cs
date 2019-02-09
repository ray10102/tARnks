﻿using UnityEngine;
using UnityEngine.EventSystems;

public class FloatingJoystick : Joystick
{
    Vector2 joystickCenter = Vector2.zero;

    void Start()
    {
        background.gameObject.SetActive(false);
        if (isRight)
        {
            rightJoystick = this;
        }
        else
        {
            leftJoystick = this;
        }
    }

    public override void OnDrag(PointerEventData eventData)
    {
        if (!isRight)
        {
            Vector2 direction = eventData.position - joystickCenter;
            inputVector = (direction.magnitude > background.sizeDelta.x / 2f) ? direction.normalized : direction / (background.sizeDelta.x / 2f);
            ClampJoystick();
            handle.anchoredPosition = (inputVector * background.sizeDelta.x / 2f) * handleLimit;
        }
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        background.position = eventData.position;
        handle.anchoredPosition = Vector2.zero;
        joystickCenter = eventData.position;

        if (isRight) 
        {

        }
    }

    public override void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        inputVector = Vector2.zero;
    }
}