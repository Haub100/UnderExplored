using UnityEngine;
using System.Collections;

public class colliderAppear : MonoBehaviour {

	public GameObject indicator;
	public GameObject colliderToDissappear;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(indicator.GetComponent<ProgressionIndicator>().getIsProgressed()){
			colliderToDissappear.SetActive(true);
		}
		else{
			colliderToDissappear.SetActive(false);
		}
	}
}
