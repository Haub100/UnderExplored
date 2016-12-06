using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Pause_Main : MonoBehaviour {

	private GameObject RoomManager;
	//public Transform canvas;
	private GameObject Player;

	// Use this for initialization
	void Start () {
		RoomManager = GameObject.Find("RoomManager");
		Player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("escape") && RoomManager.GetComponent<Mouse_Control>().getIsMouseLocked()){
			RoomManager.GetComponent<Mouse_Control>().setIsMouseLocked(false);
			Time.timeScale = 0;
			Player.GetComponent<RigidbodyFirstPersonController>().enabled = false;
		} else if(Input.GetKeyDown("escape") && !RoomManager.GetComponent<Mouse_Control>().getIsMouseLocked()){
			RoomManager.GetComponent<Mouse_Control>().setIsMouseLocked(true);
			Time.timeScale = 1;
			Player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
		}
	}
}
