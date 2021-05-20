using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger2way : MonoBehaviour {

	/* WIP -- Want to make a door that will swing in either direction based on the side the player is accessing it from
	 *  notes- 
	 * use collision to detect player
	 * if they are in it the are on one side of the door , if not then they are on the other side
	 * 
	 * 
	*/
//	public GameObject Door;
////	public DoorType doorStyle;
//	public bool useOpenRange;//uses the gameObjects door script to determine when to trigger - set the door scripts openRange to change the distance
//	//public bool twoWayDoor; //only works for hinged doors
//	public GameObject UI;// the Canvas where our UIText lives - will set this to active
//	public Text UIText; //what text gameobject we want to control color/size/placement of our text
//	string openMessage = "Use "; //set here the text we want to appear before the button text
//	//string closeMessage = "Close with ";//set here the text we want to appear before the button text
//	public string openButton = "v"; //choose a keycode we would like for opening the door
//	//public string closeButton = "c";//choose a keycode we would like for closing the door
//
//	float state;
//	float UIDelay;
//	float UITimer;
//	float delay;
//	float timer;
//	// Use this for initialization
//	void Start () {
//	}
//
//	// Update is called once per frame
//	public void OnTriggerEnter(Collider other)
//	{
//		if (Door.GetComponent<Door>().type == Door.DoorType.HingeUp) 
//		{
//			Door.GetComponent<Door>().type = Door.DoorType.HingeDown;
//		}
//	}
//
//	public void OnTriggerExit(Collider other)
//	{
//
//	}
//
//	void Update () {
//		if (useOpenRange == true) {
//			if (gameObject.GetComponent<Door>().activateTarget != null) {
//				if (Vector3.Distance (gameObject.GetComponent<Door>().activateTarget.transform.position, gameObject.GetComponent<Door>().player.transform.position) <= gameObject.GetComponent<Door>().openRange) {
//					state = 1;
//				} else {
//					state = 0;
//				}
//			} else 
//			{
//				if (Vector3.Distance (transform.position, gameObject.GetComponent<Door>().player.transform.position) <= gameObject.GetComponent<Door>().openRange) {
//					state = 1;
//				} else 
//				{
//					state = 0;
//				}
//			}
//		}
//		if (state == 0) {
//			if (UI != null)
//			{				
//				UI.SetActive (false);
//			}
//
//		}
//
//		if (state == 1)
//		{
//			if (UI != null) 
//			{
//				if (gameObject.GetComponent<Door>().isOpen == true) 
//				{
//					//UIText.text = closeMessage + closeButton;
//				} else 
//				{
//					UIText.text = openMessage + openButton;
//				}
//				if (useOpenRange == true) {
//					if (gameObject.GetComponent<Door>().activateTarget != null) {
//						if (Vector3.Distance (gameObject.GetComponent<Door>().activateTarget.transform.position, gameObject.GetComponent<Door>().player.transform.position) <= gameObject.GetComponent<Door>().openRange) {
//							if (UITimer <= Time.time)
//							{
//								UI.SetActive (true);
//							}
//						} 
//						else 
//						{
//							UI.SetActive (false);
//						}
//					}
//				}
//
//			}
//			//on button press do this stuff
//			if (delay == 0) {				
//				if (Input.GetKey (openButton)) {
//					if (UI != null)
//					{
//						UI.SetActive (false);
//					}
//					gameObject.GetComponent<Door>().triggerOpen = true;
//					timer = Time.time + .4f;
//					UITimer = Time.time + 1.2f;
//					delay = 1;
//				}
////				if (Input.GetKey (closeButton)) 
////				{
////					if (UI != null)
////					{
////						UI.SetActive (false);
////					}
////					gameObject.GetComponent<Door>().triggerClose = true;
////					timer = Time.time + .4f;
////					UITimer = Time.time + 1.2f;
////					delay = 1;
////				}
//			}
//			if (delay == 1) {
//				if (timer <= Time.time)
//				{
//					delay = 0;
//				}
//			}
//
//
//		}
//	}
}
