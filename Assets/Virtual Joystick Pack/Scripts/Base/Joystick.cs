using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [Header("Options")]
    [Range(0f, 2f)] public float handleLimit = 1f;
    public JoystickMode joystickMode = JoystickMode.AllAxis;
    public bool isRight = false;

    public static Joystick leftJoystick;
    public static Joystick rightJoystick;

    protected Vector2 inputVector = Vector2.zero;

    [Header("Components")]
    public RectTransform background;
    public RectTransform handle;
    public bool pressed;

    public float Horizontal { get { return inputVector.x; } }
    public float Vertical { get { return inputVector.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }


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

    public virtual void OnDrag(PointerEventData eventData)
    {
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnMouseDown()
    {
        pressed = true;
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
    }

    protected void ClampJoystick()
    {
        if (joystickMode == JoystickMode.Horizontal)
            inputVector = new Vector2(inputVector.x, 0f);
        if (joystickMode == JoystickMode.Vertical)
            inputVector = new Vector2(0f, inputVector.y);
    }
}

public enum JoystickMode { AllAxis, Horizontal, Vertical}
