using UnityEngine;
using UnityEngine.InputSystem;

public class CameraResetPosition : MonoBehaviour
{
    public InputAction InputBindings;
    
    private void OnEnable()
    {
        InputBindings.Enable();
        InputBindings.performed += InputBindingsOnperformed;
    }
    
    private void OnDisable()
    {
        InputBindings.performed -= InputBindingsOnperformed;
        InputBindings.Disable();
    }

    private void InputBindingsOnperformed(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValueAsButton())
        {
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }
}
