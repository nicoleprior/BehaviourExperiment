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
	public class StopScreenCapture : MonoBehaviour
    {
 
	    [SerializeField]
	    public KeyCode selectedKey = KeyCode.None;

	    [SerializeField]
	    public GameObject invokerscript;

	    void Update()
	    {
		    if (Input.GetKeyDown(selectedKey))
		    {
			    var references = invokerscript.GetComponents<ScreenCapture>();

	        	foreach (var reference in references)
	        	{
		        	 reference.StopRepeating();
	        	}
	          
	    	}

        }

      }
 }
