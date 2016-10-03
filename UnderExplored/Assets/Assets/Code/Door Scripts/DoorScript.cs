using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {


    public GameObject door; // The door object you would like to manipulate (through position or animation)
    Animation anim;

    // Use this for initialization
    void Start () {
        anim = door.GetComponent<Animation>();
        anim.Stop();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Open a closed door 
    void OpenDoor()
    {
        print("Door Opening");
        anim.Play("Door Opening");
        //anim.Play("Door Open");
    }

    // Close an open door
    void CloseDoor()
    {
        print("Door Closing");
        anim.Play("Door Closing");
        //anim.Play("Door Closed");
    }
}
