using UnityEngine;
using UnityEngine.InputSystem;

// Let us rotate camera up and down
public class CameraLookUpDown : MonoBehaviour
{
    public float Speed = 45f;
    public float MinAngle = -45f;
    public float MaxAngle = 45f;
    public InputAction InputBindings;

    private float rotation;
    
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
        rotation -= value;
        rotation = Mathf.Clamp(rotation, MinAngle, MaxAngle);

        var euler = transform.localEulerAngles;
        euler.x = rotation;
        transform.localEulerAngles = euler;
    }
}
