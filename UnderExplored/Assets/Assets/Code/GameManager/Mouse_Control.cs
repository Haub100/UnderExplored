using UnityEngine;
using System.Collections;

public class Mouse_Control : MonoBehaviour {

	private bool isMouseLocked;

	// Use this for initialization
	void Start () {
		setIsMouseLocked(true);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Getters
	public bool getIsMouseLocked(){
		return isMouseLocked;
	}

	// Setters
	public void setIsMouseLocked(bool isLocked){
		isMouseLocked = isLocked;

		if(isMouseLocked){
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Debug.Log("Cursor should be invisible");
			Debug.Log(isMouseLocked);
		} else{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			Debug.Log("Cursor should be visible");
			Debug.Log(isMouseLocked);
		}
	}
}
