using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DoorManager : MonoBehaviour
{
    public GameObject activeDoorFrame; //doorframe the player is currently trying to get through

    // Use this for initialization
    void Start()
    {
		Physics.IgnoreLayerCollision(12, 11, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

	public void setActiveDoor(GameObject doorFrame){
		activeDoorFrame = doorFrame;
	}

	public GameObject getActiveDoorFrame(){
		return activeDoorFrame;
	}
}
