using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeController : MonoBehaviour {

	public List<GameObject> Nodes;
	Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckNodes();
	}

	private bool CheckNodes(){
		int count = 0;
		foreach (GameObject node in Nodes){
			if(node.GetComponent<LightNode>().isLit){
				Debug.Log(Nodes.Count);
				count += 1;
			}
		}

		if (count == Nodes.Count){
			
			Debug.Log("Happening");
			animator.SetBool("isOpen", true);
			return true;
		}
		else{
			animator.SetBool("isOpen", false);
		}

		return false;
	}
}
