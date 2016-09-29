using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	private int Torches;
	private int TorchCapacity;
	public GameObject TorchUIText;

	// Use this for initialization
	void Start () {
		Torches = 5;
		TorchCapacity = 5;
		TorchUIText.GetComponent<Text>().text = Torches.ToString();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getTorches(){
		return Torches;
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
		TorchUIText.GetComponent<Text>().text = Torches.ToString();
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
		TorchUIText.GetComponent<Text>().text = Torches.ToString();
		return Torches;
	}
}
