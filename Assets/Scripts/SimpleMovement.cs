using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w")) transform.position += transform.forward * 3 * Time.deltaTime;
        if (Input.GetKey("s")) transform.position -= transform.forward * 3 * Time.deltaTime;
        if (Input.GetKey("d")) transform.position += transform.right * 3 * Time.deltaTime;
        if (Input.GetKey("a")) transform.position -= transform.right * 3 * Time.deltaTime;
    }
}
