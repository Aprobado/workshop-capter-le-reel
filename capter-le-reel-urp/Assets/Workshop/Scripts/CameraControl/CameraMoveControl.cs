using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraMoveControl : MonoBehaviour
{
    public enum DirectionAxis
    {
        FRONT_BACK,
        LEFT_RIGHT,
        UP_DOWN
    }

    public DirectionAxis Direction;
    public float Speed = 1f;
    [Tooltip("Limits are in world space")]
    public bool useLimits;
    public float MinValue = 0f;
    public float MaxValue = 0f;
    public InputAction InputBindings;
    
    private void OnEnable()
    {
        InputBindings.Enable();
    }

    private void OnDisable()
    {
        InputBindings.Disable();
    }
    
    void Update()
    {
        var value = InputBindings.ReadValue<float>() * Speed * Time.deltaTime;
        var destination = transform.position + axis * value;
        if (useLimits && MinValue < MaxValue)
        {
            switch (Direction)
            {
                case DirectionAxis.FRONT_BACK:
                    destination.z = Mathf.Clamp(destination.z, MinValue, MaxValue);
                    break;
                case DirectionAxis.LEFT_RIGHT:
                    destination.x = Mathf.Clamp(destination.x, MinValue, MaxValue);
                    break;
                case DirectionAxis.UP_DOWN:
                    destination.y = Mathf.Clamp(destination.y, MinValue, MaxValue);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        transform.position = destination;
    }

    private Vector3 axis
    {
        get
        {
            switch (Direction)
            {
                case DirectionAxis.FRONT_BACK:
                    return transform.forward;
                case DirectionAxis.LEFT_RIGHT:
                    return transform.right;
                case DirectionAxis.UP_DOWN:
                    return transform.up;
            }
            return transform.forward;
        }
    }
}
