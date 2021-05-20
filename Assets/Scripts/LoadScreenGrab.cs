using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using PivecLabs.Tools;

public class LoadScreenGrab : MonoBehaviour
{
    public GameObject Screen;
    //public ScreenCapture ScreenCaptureScript;

    // Start is called before the first frame update
    void Start()
    {

     }

    // Update is called once per frame
    void Update()
    {
        if (PivecLabs.Tools.ScreenCapture.ScreenShots != null && PivecLabs.Tools.ScreenCapture.ScreenShots.Count > 0)
            LoadImage(); 

    }

    void LoadImage()
    {
        //string RandomScreenShot = ScreenCapture.
        string screenShotFileName = PivecLabs.Tools.ScreenCapture.ScreenShots
            [Random.Range(0, PivecLabs.Tools.ScreenCapture.ScreenShots.Count)];
        Debug.Log(screenShotFileName);
        string Path = Application.persistentDataPath + "/" + "camera637535672497113793.png";

        if (!Application.isMobilePlatform)
        {
            Path = "camera637535672497113793.png";

        }

        var bytes = File.ReadAllBytes(screenShotFileName);
        Texture2D Texture = Texture2D.whiteTexture;
        //ImageConversion.LoadImage () 
        Texture.LoadImage(bytes);
        Screen.GetComponent<Renderer>().material.mainTexture = Texture;
    }
}
