using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerText()
    {
        Invoke("disappearText", 2f);
    }
    public void disappearText()
    {
        this.gameObject.SetActive(false);
    }
  }
