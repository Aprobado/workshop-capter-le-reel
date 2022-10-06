using UnityEngine;
using UnityEngine.InputSystem;

// Let us rotate camera left and right
public class CameraLookLeftRight : MonoBehaviour
{
    public float Speed = 90f;
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
        rotation += value;
        rotation = Mathf.Repeat(rotation, 360f);

        var euler = transform.localEulerAngles;
        euler.y = rotation;
        transform.localEulerAngles = euler;
    }
}
