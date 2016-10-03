using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

    public GameObject door; // The door object you would like to manipulate (through position or animation)

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Open a door door 
    void OpenDoor()
    {
        print("Door Opening");
    }

    void CloseDoor()
    {
        print("Door Closing");
    }
}
