using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeController : MonoBehaviour {

	public List<GameObject> Nodes;
	Animator animator;
	private bool isFullyLit;
	private int litCount;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckNodes();
	}

	public bool getIsFullyLit(){
		return isFullyLit;
	}
	public int getLitCount(){
		return litCount;
	}

	private bool CheckNodes(){
		int count = 0;
		foreach (GameObject node in Nodes){
			if(node.GetComponent<LightNode>().isLit){
				count += 1;
			}
		}

		if (count == Nodes.Count){
			animator.SetBool("isOpen", true);
			isFullyLit = true;
			litCount = count;
			return true;
		}
		else{
			animator.SetBool("isOpen", false);
			isFullyLit = false;
		}
		litCount = count;
		return false;
	}
}
