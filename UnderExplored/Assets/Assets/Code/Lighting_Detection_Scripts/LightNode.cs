using UnityEngine;
using System.Collections;

public class LightNode : MonoBehaviour {

	public float litPercentage;
	public bool isLit;

/*
    [SerializeField]
    List<GameObject> torches_detected; // List of all torches

    [SerializeField]
    bool conditionsFulfilled; // Are there enough torches to fulfill the conditions set in the inspector?
*/
    // Use this for initialization

    void Start(){
        isLit = false;
        litPercentage = 0f;
    }

    // When a torch enters into the node's detection area or if the torch pops back up again.
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Torch"))
        {
            print("Torch Detected in Node Area");
            //torches_detected.Add(col.gameObject); // add the torch to the list of detected torches
            litPercentageIncrease(col.gameObject.GetComponent<Transform>().position);
        }
    }

    // When a torch exits the node's detection area
    // Issue: If the torch is setInactive it won't be removed.
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Torch"))
        {
            print("Torch Removed From Node Area");
            //torches_detected.Remove(col.gameObject); // add the torch to the list of detected torches
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          Checking if Conditions are fulfilled (i.e. are there enough torches lit to change the node state?)
    //          Node state could also be a collider
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void conditionsCheck()
    {
        if (litPercentage > 1)
            isLit = true;
        else
            isLit = false;;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          Gets the distance between the node and the torch to calculate lit percentage increase or decrease
    //          Adds to the litPercentage
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void litPercentageIncrease(Vector3 torchLocation){
        float distance = Vector3.Distance(this.transform.position, torchLocation);
        
        if(distance <= 3){
            litPercentage += 1f;
        } else if (3 < distance && distance <= 5){
            litPercentage += 0.5f;
        } else{
            litPercentage += 0.35f;
        }

        conditionsCheck();
    }

    // overload in case we want to add lighting boost modifiers
    public void litPercentageIncrease(Vector3 torchLocation, float modifier){
        float distance = Vector3.Distance(this.transform.position, torchLocation);
        
        if(distance <= 3){
            litPercentage += 1f;
        } else if (3 < distance && distance <= 5){
            litPercentage += 0.5f;
        } else{
            litPercentage += 0.35f;
        }

        litPercentage += modifier;
        conditionsCheck();
    }

    public void litPercentageDecrease(Vector3 torchLocation){
        float distance = Vector3.Distance(this.transform.position, torchLocation);
        
        if(distance <= 3){
            litPercentage -= 1f;
        } else if (3 < distance && distance <= 5){
            litPercentage -= 0.5f;
        } else{
            litPercentage -= 0.35f;
        }
        conditionsCheck();
    }

    // overload in case we want to add lighting boost modifiers
    public void litPercentageDecrease(Vector3 torchLocation, float modifier){
        float distance = Vector3.Distance(this.transform.position, torchLocation);
        
        if(distance <= 3){
            litPercentage -= 1f;
        } else if (3 < distance && distance <= 5){
            litPercentage -= 0.5f;
        } else{
            litPercentage -= 0.35f;
        }

        litPercentage -= modifier;
        conditionsCheck();
    }
}
