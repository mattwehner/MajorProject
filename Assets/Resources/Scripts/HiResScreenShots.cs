using System;
using System.IO;
using UnityEngine;

public class HiResScreenShots : MonoBehaviour
{
    public int resHeight = 2232;
    public int resWidth = 4096;

    private void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            var rt = new RenderTexture(resWidth, resHeight, 24);
            GetComponent<Camera>().targetTexture = rt;
            var screenShot = new Texture2D(resWidth, resHeight,
                TextureFormat.RGB24, false);
            GetComponent<Camera>().Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0);
            GetComponent<Camera>().targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors 
            Destroy(rt);
            var bytes = screenShot.EncodeToPNG();
            var filename = Application.dataPath + "/screenshots/screen"
                           + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png";
            File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
        }
    }
}