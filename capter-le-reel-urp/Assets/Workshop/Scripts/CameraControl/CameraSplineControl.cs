using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Splines;

// Let us move along a Spline Object via a SplineAnimate script
public class CameraSplineControl : MonoBehaviour
{
    public float Speed = .1f;
    public SplineAnimate SplineController;
    public InputAction InputBindings;
    
    private void OnEnable()
    {
        InputBindings.Enable();
    }

    private void OnDisable()
    {
        InputBindings.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        var move = InputBindings.ReadValue<float>();
        var newTime = SplineController.normalizedTime + move * Speed * Time.deltaTime;
        if (SplineController.loopMode == SplineAnimate.LoopMode.Once)
        {
            newTime = Mathf.Clamp01(newTime);
        }
        SplineController.normalizedTime = newTime;
    }
}
