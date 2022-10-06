using UnityEngine;

public class Screenshots : MonoBehaviour
{
    private Camera cam;
    private bool takeHiResShot = false;

    public RenderTexture rtTarget;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    public static string ScreenShotName(int width, int height) {
        return string.Format("{0}/Screenshots/screen_{1}x{2}_{3}.png", 
            Application.dataPath.Replace("/Assets", ""), 
            width, height, 
            System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }
    
    public void TakeHiResShot() {
        takeHiResShot = true;
    }
    
    void LateUpdate() {
        if (!Application.isEditor)
        {
            enabled = false;
            return;
        }
        takeHiResShot |= Input.GetKeyDown("k");
        if (takeHiResShot)
        {
            if (cam.targetTexture != null)
            {
                var rt = cam.targetTexture;
                var screenShot = new Texture2D(400, 240, TextureFormat.RGB24, false);
                
                var previousRT = RenderTexture.active;
                RenderTexture.active = rt;
                // ReadPixels reads from the currently active render texture
                screenShot.ReadPixels(new Rect(0,0,rt.width, rt.height), 0, 0);
                screenShot.Apply();
                RenderTexture.active = previousRT;
                
                var bytes = screenShot.EncodeToPNG();
                var filename = ScreenShotName(400, 240);
                System.IO.File.WriteAllBytes(filename, bytes);
                Debug.Log($"Took screenshot to: {filename}");
            }
            else if (rtTarget != null)
            {
                var rt = rtTarget;
                var screenShot = new Texture2D(400, 240, TextureFormat.RGB24, false);
                
                var previousRT = RenderTexture.active;
                RenderTexture.active = rt;
                cam.Render();
                // ReadPixels reads from the currently active render texture
                screenShot.ReadPixels(new Rect(0,0,rt.width, rt.height), 0, 0);
                screenShot.Apply();
                RenderTexture.active = previousRT;
                
                var bytes = screenShot.EncodeToPNG();
                var filename = ScreenShotName(400, 240);
                System.IO.File.WriteAllBytes(filename, bytes);
                Debug.Log($"Took screenshot to: {filename}");
            }
            else
            {
                ScreenCapture.CaptureScreenshot(ScreenShotName(Screen.width, Screen.height));
            }
            takeHiResShot = false;
        }
    }
}