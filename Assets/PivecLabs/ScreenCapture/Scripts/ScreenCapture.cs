namespace PivecLabs.Tools
{
        using System.Collections;
        using System.Collections.Generic;
        using UnityEngine;
        using UnityEngine.Events;
        using UnityEngine.UI;
        using UnityEngine.Video;
 		using System.Linq;
		
#if UNITY_EDITOR
    using UnityEditor;
#endif



	
    [AddComponentMenu("")]
	public class ScreenCapture : MonoBehaviour
	{
	 [SerializeField]
		public enum ENCODE
	    {
		    png,
		    jpg,
		    tga
	    }
	    
	 [SerializeField]
		public ENCODE imageformat = ENCODE.png;
	 [SerializeField]
		public bool hires;
	 [SerializeField]
		public string imagePath;
	 [SerializeField]
		public bool logdata;
	    [Range(0.0f,10f)]
	 [SerializeField]
	    public float timer = 0;
	    [Range(0,60)]
	 [SerializeField]
	    public float repeat = 0;
	    
	    private byte[] imageBytes;
	    private string screenShot;
	    
	    int resWidth = Screen.width*4; 
	    int resHeight = Screen.height*4;
	 [SerializeField]
		public Camera camera;
	    int scale = 1;
	    RenderTexture renderTexture;
	 [SerializeField]
		public KeyCode selectedKey = KeyCode.None;

	 [SerializeField]
		public bool displayCanvas;

	 [SerializeField]
		public Canvas canvas;




		public static List<string> ScreenShots;




        // EXECUTABLE: ----------------------------------------------------------------------------

		void Update()
		{
			if (Input.GetKeyDown(selectedKey))
			{
	        
	        
	        if (hires == true)
	        {
        	
		        if (repeat > 0)
		        {
			        InvokeRepeating("captureHiRes", 0.0f, repeat);
		        
		        }
		        else
		        {
		        	captureHiRes();
		        }
	        }
	        else 
	        {
	        	
		        if (repeat > 0)
		        {
			        InvokeRepeating("captureLoRes", 0.0f, repeat);
		        
		        }
		        else
		        {
		        	captureLoRes();
		        }

	        }
			}
	 
        }

       
	    private void captureLoRes()
	    {
	    	StartCoroutine(captureScreenshot());
	    }
	    
	    private void captureHiRes()
	    {
		    StartCoroutine(captureHiResScreenshot());
	    }
	    
	    public void StopRepeating()
	    {
		    CancelInvoke("captureLoRes");
		    CancelInvoke("captureHiRes");
	    }


	    IEnumerator captureScreenshot()
	    {
		    yield return new WaitForSeconds(timer);
		    yield return new WaitForEndOfFrame();
		    	        
		    screenShot = string.Format("{0}{1}{2}{3}", imagePath,System.DateTime.Now.Ticks,".",imageformat);

		    Texture2D screenImage = new Texture2D(Screen.width, Screen.height);
		    screenImage.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
		    screenImage.Apply();
		    
		    switch (this.imageformat)
		    {
		    case ENCODE.png:
			    imageBytes = screenImage.EncodeToPNG();
			    break;
		    case ENCODE.jpg:
			    imageBytes = screenImage.EncodeToJPG();
			    break;
		    case ENCODE.tga:
			    imageBytes = screenImage.EncodeToTGA();
			    break;
		    }
		    
		    if(Application.isMobilePlatform)
		    {
			    System.IO.File.WriteAllBytes(Application.persistentDataPath+"/"+screenShot, imageBytes);

		    }
		    else
		    {
		    	
			    System.IO.File.WriteAllBytes(screenShot, imageBytes);

		    }
		    if (logdata == true)
		    {
			    Debug.Log("screenImage.width" + screenImage.width);		
			    Debug.Log("imagesBytes=" + imageBytes.Length);
			    Debug.Log("imagePath=" + screenShot);
		    }

			if (ScreenShots == null)
				ScreenShots = new List<string>();

			ScreenShots.Add(screenShot);

		    if (displayCanvas == true)
		    {
			    CanvasGroup cGroup = canvas.GetComponent<CanvasGroup>();
			    if (cGroup != null)
			    {
					float startTime = Time.unscaledTime;

					    WaitUntil waitUntil = new WaitUntil(() =>
					    {
						    float t = (Time.unscaledTime - startTime) / 1.5f;
						    cGroup.alpha = Mathf.Lerp(
							    0.0f,
							    1.0f, 
							    t
						    );

						    return t > 1.0f;
					    });
				
				    yield return waitUntil;
			    }
			    if (cGroup != null)
			    {
				    float startTime = Time.unscaledTime;

				    WaitUntil waitUntil = new WaitUntil(() =>
				    {
					    float t = (Time.unscaledTime - startTime) / 1.5f;
					    cGroup.alpha = Mathf.Lerp(
						    1.0f,
						    0.0f, 
						    t
					    );

					    return t > 1.0f;
				    });
				    yield return waitUntil;
			    }
		    }

	    }
	    
	    IEnumerator captureHiResScreenshot()
	    {

		    yield return new WaitForSeconds(timer);
		    yield return new WaitForEndOfFrame();
	        
		    screenShot = string.Format("{0}{1}{2}{3}", imagePath,System.DateTime.Now.Ticks,".",imageformat);

	    	int resWidthN = resWidth*scale;
	    	int resHeightN = resHeight*scale;
	    	RenderTexture rt = new RenderTexture(resWidthN, resHeightN, 24);
	    	camera.targetTexture = rt;

	    	Texture2D screenImage = new Texture2D(resWidthN, resHeightN, TextureFormat.RGB24,false);
	    	camera.Render();
		    RenderTexture.active = rt;
	    	screenImage.ReadPixels(new Rect(0, 0, resWidthN, resHeightN), 0, 0);
	    	camera.targetTexture = null;
		    RenderTexture.active = null; 
	    
		    switch (this.imageformat)
		    {
		    case ENCODE.png:
			    imageBytes = screenImage.EncodeToPNG();
			    break;
		    case ENCODE.jpg:
			    imageBytes = screenImage.EncodeToJPG();
			    break;
		    case ENCODE.tga:
			    imageBytes = screenImage.EncodeToTGA();
			    break;
		    }
		    
		    if(Application.isMobilePlatform)
		    {
			    System.IO.File.WriteAllBytes(Application.persistentDataPath+"/"+screenShot, imageBytes);

		    }
		    else
		    {
		    	
			    System.IO.File.WriteAllBytes(screenShot, imageBytes);

		    }
		    if (logdata == true)
		    {
			    Debug.Log("HiResImage.width" + screenImage.width);		
			    Debug.Log("imagesBytes=" + imageBytes.Length);
			    Debug.Log("imagePath=" + screenShot);
		    }
	    
		    if (displayCanvas == true)
		    {
			    CanvasGroup cGroup = canvas.GetComponent<CanvasGroup>();
			    if (cGroup != null)
			    {
				    float startTime = Time.unscaledTime;

				    WaitUntil waitUntil = new WaitUntil(() =>
				    {
					    float t = (Time.unscaledTime - startTime) / 1.5f;
					    cGroup.alpha = Mathf.Lerp(
						    0.0f,
						    1.0f, 
						    t
					    );

					    return t > 1.0f;
				    });
				
				    yield return waitUntil;
			    }
			    if (cGroup != null)
			    {
				    float startTime = Time.unscaledTime;

				    WaitUntil waitUntil = new WaitUntil(() =>
				    {
					    float t = (Time.unscaledTime - startTime) / 1.5f;
					    cGroup.alpha = Mathf.Lerp(
						    1.0f,
						    0.0f, 
						    t
					    );

					    return t > 1.0f;
				    });
				    yield return waitUntil;
			    }
		    }

	    }
	}

	//public 
}
