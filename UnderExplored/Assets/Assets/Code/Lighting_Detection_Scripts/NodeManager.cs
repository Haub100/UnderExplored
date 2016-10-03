using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NodeManager : MonoBehaviour {

    // Be sure to drag in the nodes (which you are going to be detecting) into the Inspector window
    
    public List<GameObject> nodes; // List of all nodes
    bool torchesNotCheckedYet; // Local boolean to make sure the update function doesn't keep spamming a lit boolean
    public string methodToCallWhenLit; // What sort of action do you want to take once all of the nodes are lit?
    public string methodToCallWhenNotLit; // What sort of action do you want to take once all of the nodes are NOT lit?

    // Use this for initialization
    void Start () {
        torchesNotCheckedYet = true;
	}
	
	// Update is called once per frame
	void Update () {
        // Do the below if all of your torches have successfully lit up all the nodes you care about
        if (checkAllForLit() && torchesNotCheckedYet)
        {
            torchesNotCheckedYet = false;
            SendMessage(methodToCallWhenLit);
        }
        //Do the below if the you no longer have all of the light powering your nodes anymore (Say, in case you want to close a door or deactivate a trap)
        if (!checkAllForLit() && !torchesNotCheckedYet)
        {
            torchesNotCheckedYet = true;
            SendMessage(methodToCallWhenNotLit);
        }

    }

    // Go through the nodes one by one, making sure that all of them are activated (or lit up)
    bool checkAllForLit()
    {
        foreach (GameObject node in nodes)
        {
            if (node.GetComponent<LightNode>().isLit)
            {
                //print("Node Meets Requirements");
            }
            else { return false; }
        }
        return true;
    }

   
}
