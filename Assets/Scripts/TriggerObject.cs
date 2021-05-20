using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerObject : MonoBehaviour
{
    bool hasBeenTriggered;
    public UnityEvent triggerEvent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);

        if (other.gameObject.name.Contains("Player"))
        {
            if (hasBeenTriggered)
                this.gameObject.SetActive(false);
            else
                triggerEvent.Invoke();

            hasBeenTriggered = true;
        }
    }
}

