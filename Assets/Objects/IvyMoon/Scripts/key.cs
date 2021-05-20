using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class key : MonoBehaviour {
    public GameObject player;//set your player here
    public float pickupRange = 2f;//set distance to pick up key
    public GameObject doorToUnlock;// gameObject running the doortrigger script (required) this will make the trigger activate so the door can be opened now
    private DoorTrigger trigger;//reference to the trigger on the doorToUnlock
	// Use this for initialization
	void Start () {
        trigger = doorToUnlock.GetComponent<DoorTrigger>();
	}

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) <= pickupRange)
        {
            trigger.enabled = true;
            Destroy(gameObject);
        }
    }
}
