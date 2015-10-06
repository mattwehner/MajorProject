using UnityEngine; 
using System.Collections; 

public class HiResScreenShots : MonoBehaviour { 
    public int resWidth = 4096; 
    public int resHeight = 2232; 

   void Update () { 
        if (Input.GetKeyDown("k")) { 
            RenderTexture rt = new RenderTexture(resWidth, resHeight, 24); 
            GetComponent<Camera>().targetTexture = rt; 
            Texture2D screenShot = new Texture2D(resWidth, resHeight, 
                                                 TextureFormat.RGB24, false); 
            GetComponent<Camera>().Render(); 
            RenderTexture.active = rt; 
            screenShot.ReadPixels(new Rect(0, 0, resWidth, resHeight), 0, 0); 
            GetComponent<Camera>().targetTexture = null; 
            RenderTexture.active = null; // JC: added to avoid errors 
            Destroy(rt); 
            byte[] bytes = screenShot.EncodeToPNG(); 
            string filename = Application.dataPath + "/screenshots/screen" 
                            + System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + ".png"; 
            System.IO.File.WriteAllBytes(filename, bytes); 
            Debug.Log(string.Format("Took screenshot to: {0}", filename)); 
        } 
   } 
} 