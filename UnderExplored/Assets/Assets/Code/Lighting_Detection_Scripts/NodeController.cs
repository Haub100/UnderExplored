using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class NodeController : MonoBehaviour {

	public List<GameObject> Nodes;
	[RangeAttribute(0.1f, 1f)] 
	public float percentToProgress;
	Animator animator;
	private bool isFullyLit;
	private bool isLitEnough;
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

	public bool getIsLitEnough(){
		return isLitEnough;
	}

	public int getLitCount(){
		return litCount;
	}

	public float getPercentToProgress(){
		return percentToProgress;
	}

	private bool CheckNodes(){
		int count = 0;
		double countNeeded = Nodes.Count * percentToProgress;
		double dotsToLight = Math.Round(countNeeded, 0, MidpointRounding.AwayFromZero);
		int intCountNeeded = (int)dotsToLight;
		
		foreach (GameObject node in Nodes){
			if(node.GetComponent<LightNode>().isLit){
				count += 1;
			}
		}

		//There are three states for a node controller Fully Lit | Lit Enough | Not Lit Enough
		if (count == Nodes.Count){
			animator.SetBool("isOpen", true);
			isFullyLit = true;
			isLitEnough = true;
			litCount = count;
			return true;
		}
		else if (count >= intCountNeeded){
			animator.SetBool("isOpen", true);
			litCount = count;
			isFullyLit = false;
			isLitEnough = true;
			return true;
		}
		else{
			animator.SetBool("isOpen", false);
			isFullyLit = false;
			isLitEnough = false;
		}
		litCount = count;
		return false;
	}
}
