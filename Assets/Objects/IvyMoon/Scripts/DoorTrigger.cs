using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorTrigger : MonoBehaviour {
	public bool useOpenRange;//uses the gameObjects door script to determine when to trigger - set the door scripts openRange to change the distance
	//public bool twoWayDoor; //only works for hinged doors
	public GameObject UI;// the Canvas where our UIText lives - will set this to active
	public Text UIText; //what text gameobject we want to control color/size/placement of our text
	string openMessage = "Open with "; //set here the text we want to appear before the button text
	string closeMessage = "Close with ";//set here the text we want to appear before the button text
	public bool oneButton; // will only use one button to open and close the door - this will use the openButton only
	string useMessage = "Use ";
	public string openButton = "v"; //choose a keycode we would like for opening the door
	public string closeButton = "c";//choose a keycode we would like for closing the door

	float openState;
	float state;
	float UIDelay;
	float UITimer;
	float delay;
	float timer;

    bool openDoor;
    bool closeDoor;
    bool showUI;
    List<GameObject> players;
    GameObject playerToCheck;

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
 //       print(state + "triggerState");
		if (useOpenRange == true) {
			if (gameObject.GetComponent<Door>().activateTarget != null) {
                players = gameObject.GetComponent<Door>().players;
                openDoor = false;
                //check the player is in range to see the UI prompt
                for(int i = 0; i < players.Count; i++)
                {
                    playerToCheck = players[i];
                    if (Vector3.Distance(gameObject.GetComponent<Door>().activateTarget.transform.position, playerToCheck.transform.position) <= gameObject.GetComponent<Door>().openRange)
                    {
                        openDoor = true;                      
                    }
                }
                if (openDoor == true)
                {
                    state = 1;
                }
                else
                {
                    state = 0;
                }

			} else 
			{
                players = gameObject.GetComponent<Door>().players;
                openDoor = false;
                for (int i = 0; i < players.Count; i++)
                {
                    playerToCheck = players[i];
                    if (Vector3.Distance(transform.position, playerToCheck.transform.position) <= gameObject.GetComponent<Door>().openRange)
                    {
                        openDoor = true;
                    }
                }
                if (openDoor == true)
                {
                    state = 1;
                }
                else
                {
                    state = 0;
                }

            }
		}
		if (state == 0) {
			if (UI != null)
			{				
					UI.SetActive (false);
			}
			
		}

		if (state == 1)
		{
			if (UI != null) 
			{
				if (gameObject.GetComponent<Door>().isOpen == true) {
					if (oneButton == false) {
						UIText.text = closeMessage + closeButton;
					}
				} else {
					if (oneButton == true) {
						UIText.text = useMessage + openButton;
					} else {
						UIText.text = openMessage + openButton;
					}
				}
                if (useOpenRange == true)
                {
                    if (gameObject.GetComponent<Door>().activateTarget != null)
                    {
                        showUI = false;
                        for (int i = 0; i < players.Count; i++)
                        {
                            playerToCheck = players[i];
                            if (Vector3.Distance(gameObject.GetComponent<Door>().activateTarget.transform.position, playerToCheck.transform.position) <= gameObject.GetComponent<Door>().openRange)
                            {
                                if (UITimer <= Time.time)
                                {
                                    showUI = true;
                                }
                            }
                        }
                        if (showUI == true)
                        {
                            UI.SetActive(true);
                        }
                        else
                        {
                            UI.SetActive(false);
                        }

                    }
                    else
                    {
                        showUI = false;
                        for (int i = 0; i < players.Count; i++)
                        {
                            playerToCheck = players[i];
                            if (Vector3.Distance(gameObject.GetComponent<Door>().originalPosition, playerToCheck.transform.position) <= gameObject.GetComponent<Door>().openRange)
                            {
                                if (UITimer <= Time.time)
                                {
                                    showUI = true;
                                }

                            }
                        }
                        if (showUI == true)
                        {
                            UI.SetActive(true);
                        }
                        else
                        {
                            UI.SetActive(false);
                        }
                    }
                }
			}
			//on button press do this stuff
			if (delay == 0) {				
				if (Input.GetKey (openButton) || Input.GetKey(KeyCode.JoystickButton2)) {
					if (UI != null)
					{
						UI.SetActive (false);
					}
					if (oneButton == true) 
					{
                        if(gameObject.GetComponent<Door>().isOpen == true)
                        {
                            gameObject.GetComponent<Door>().triggerClose = true;
                        }
                        else
                        {
                            gameObject.GetComponent<Door>().triggerOpen = true;
                        }
      //                  if (openState == 0) {
						//	gameObject.GetComponent<Door>().triggerOpen = true;
						//	openState = 1;
						//}
						//if (openState == 2) {
						//	gameObject.GetComponent<Door>().triggerClose = true;
						//	openState = 0;
						//}
					}
                    else
                    {
						gameObject.GetComponent<Door>().triggerOpen = true;
					}
					timer = Time.time + .4f;
					UITimer = Time.time + 1.2f;
					delay = 1;
				}
				if (oneButton == false) {
					if (Input.GetKey (closeButton)) {
						if (UI != null) {
							UI.SetActive (false);
						}
						gameObject.GetComponent<Door>().triggerClose = true;
						timer = Time.time + .4f;
						UITimer = Time.time + 1.2f;
						delay = 1;
					}
				}
			}
			if (delay == 1) {
				if (timer <= Time.time)
				{
					if (openState == 1){openState = 2;}
					delay = 0;
				}
			}


		}
	}
}
