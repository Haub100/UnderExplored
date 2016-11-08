using UnityEngine;
using System.Collections;

public class trapFloor : MonoBehaviour {

	Animator crumbleAnimation;
	bool isCrumbled = false;

	// Use this for initialization
	void Start () {
		crumbleAnimation = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void setIsCrumbled(bool crumbled){
		isCrumbled = crumbled;
	}

	void OnTriggerEnter(Collider col){
		if(col.gameObject.CompareTag("Player") && !isCrumbled){
			Debug.Log("Trig Enter");
			isCrumbled = true;
			crumbleAnimation.SetBool("isCrumbled", true);
		}
	}
}
