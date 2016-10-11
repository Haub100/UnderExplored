using UnityEngine;
using System.Collections;

public class TorchSource : MonoBehaviour {

	public int torchCount; //number of torches available in the source

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getTorchCount(){
		return torchCount; 
	}

	public int takeTorches(int torchesToTake){
		if(torchesToTake <= torchCount){
			torchCount -= torchesToTake;
			return torchesToTake;
		}
		else{
			int returnAmount = torchCount;
			torchCount -= torchCount;
			return returnAmount;
		}
	}
}
