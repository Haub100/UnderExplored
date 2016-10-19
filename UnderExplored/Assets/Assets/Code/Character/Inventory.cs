using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	private int Torches;
	private int TorchCapacity;
	private GameObject TorchCount;

	// Use this for initialization
	void Start () {
		Torches = 2;
		TorchCapacity = 10;
		TorchCount = GameObject.Find("TorchCount");
		TorchCount.GetComponent<Text>().text = Torches.ToString()+ "/" + TorchCapacity;
	}
	
	// Update is called once per frame
	void Update () {
		if(Torches == 0){
			//Debug.Log("here");
		}
	}

	public int getTorches(){
		return Torches;
	}
	public bool isFull(){
		if(TorchCapacity == Torches){
			return true;
		}
		return false;
	}

	public int addTorches(int torchesAdded){
		if(Torches < TorchCapacity){
			Torches += torchesAdded;
			if(Torches > TorchCapacity){
				Torches = TorchCapacity;
			}
		}
		else{
			//torches is at maximum capacity
		}
		TorchCount.GetComponent<Text>().text = Torches.ToString() + "/" + TorchCapacity;
		return Torches;		
	}

	public int removeTorches(int torchesRemoved){
		if(Torches > 0){
			Torches -= torchesRemoved;
			if(Torches < 0){
				Torches = 0;
			}
		}
		else{
			//torches already zero torches
		}
		TorchCount.GetComponent<Text>().text = Torches.ToString() + "/" + TorchCapacity;
		return Torches;
	}

	public int torchesNeeded(){
		return (TorchCapacity - Torches);
	}
}
