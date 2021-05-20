using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneTest : MonoBehaviour
{
    public AudioClip recordedClip;



    public void record() 
    {
        // Record for X number of seconds to an audio clip
        string[] microphoneList = Microphone.devices;
        Debug.Log (microphoneList[0]);
        recordedClip = Microphone.Start(microphoneList[0], true, 10, 44100);
    }

    // Start is called before the first frame update
    void Start()
    {
        record();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
