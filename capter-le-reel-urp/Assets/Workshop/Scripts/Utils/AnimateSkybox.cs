using UnityEngine;

public class AnimateSkybox : MonoBehaviour
{
    private static readonly int Rotation = Shader.PropertyToID("_Rotation");

    void Update()
    {
        if (RenderSettings.skybox == null)
        {
            enabled = false;
            return;
        }
        RenderSettings.skybox.SetFloat(Rotation, Mathf.Repeat(Time.time * 3f, 360f));
    }
}
