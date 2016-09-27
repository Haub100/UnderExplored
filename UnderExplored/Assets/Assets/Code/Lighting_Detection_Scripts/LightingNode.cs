using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Issues:
// 1. The code underneath update for removing torches that are set inactive is resource intensive.
// 2. What if the torch is destroyed? Like during an instantiation?
//      Possible Solution. Torches are never destroyed in a scene. For instance,
//      a torch can be picked up and reused elsewhere in the scene (put into storage, for instance), but not destroyed
// 3. You need to delete the list from memory at the end of the scene/Whenever it isn't needed anymore.


public class LightingNode : MonoBehaviour
{
    public int torchesNeeded; // The number of torches you need for this node to be lit (fulfilled)!

    [SerializeField]
    List<GameObject> torches_detected; // List of all torches

    [SerializeField]
    bool conditionsFulfilled; // Are there enough torches to fulfill the conditions set in the inspector?

    // Use this for initialization
    void Start()
    {
        torches_detected = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        removeInactiveObjects(); // remove any objects that have deactivated during gameplay for any reason.
        conditionsFulfilled = conditionsCheck(); // check and verify conditions

    }

    // When a torch enters into the node's detection area or if the torch pops back up again.
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Torch"))
        {
            print("Torch Detected in Node Area");
            torches_detected.Add(col.gameObject); // add the torch to the list of detected torches
        }
    }

    // When a torch exits the node's detection area
    // Issue: If the torch is setInactive it won't be removed.
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Torch"))
        {
            print("Torch Removed From Node Area");
            torches_detected.Remove(col.gameObject); // add the torch to the list of detected torches
        }
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////
    //      Note: This is a way to solve the problem of removing torches that are set inactive
    //      However, it is very slow and may not update fast enough. Look for alternate solution
    //      This way though, you will not need to create a new "temp List" and then go about deleting it
    /////////////////////////////////////////////////////////////////////////////////////////////////////
    void removeInactiveObjects()
    {
        GameObject temp = null; // Store a item here for deletion
        foreach (GameObject torch in torches_detected)
        {
            if (!torch.activeSelf) //if the torch is not active
            {
                print("A Torch has been deactivated.");
                temp = torch;
            }
        }
        if (temp != null)
            torches_detected.Remove(temp);
    }



    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          Checking if Conditions are fulfilled (i.e. are there enough torches lit to change the node state?)
    //          Node state could also be a collider
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    bool conditionsCheck()
    {
        if (torches_detected.Count >= torchesNeeded)
            return true;
        else
            return false;
    }
}
