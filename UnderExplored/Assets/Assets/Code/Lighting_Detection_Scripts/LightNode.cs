using UnityEngine;
using System.Collections;

public class LightNode : MonoBehaviour
{

    public int litPercentage;
    public bool isLit;

    private float closeDistance;
    private float mediumDistance;
    private int ghostSpawnings;

    // Use this for initialization

    void Start()
    {
        isLit = false;
        litPercentage = 0;
        closeDistance = 30;
        ghostSpawnings = 0;
    }

    // When a torch enters into the node's detection area or if the torch pops back up again.
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("LightSource"))
        {
            print("LightSource Detected in Node Area");
            //litPercentageIncrease(col.gameObject.GetComponent<Transform>().position);
        }
    }

    // When a torch exits the node's detection area
    // Issue: If the torch is setInactive it won't be removed.
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("LightSource"))
        {
            print("LightSource Removed From Node Area");
            //litPercentageDecrease(col.gameObject.GetComponent<Transform>().position);
        }
    }

    public int getGhostSpawnings(){
        return ghostSpawnings;   
    }
    public void incrementGhostSpawnings(){
        ghostSpawnings += 1;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          Checking if Conditions are fulfilled (i.e. are there enough torches lit to change the node state?)
    //          Node state could also be a collider
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void conditionsCheck()
    {
        if (litPercentage >= 100)
            isLit = true;
        else
            isLit = false; ;
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          Gets the distance between the node and the torch to calculate lit percentage increase or decrease
    //          Adds to the litPercentage
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    public void litPercentageIncrease(Vector3 torchLocation)
    {
        float distance = Vector3.Distance(this.transform.position, torchLocation);

        if (distance <= closeDistance)
        {
            litPercentage += 100;
        }
        else if (3 < distance && distance <= 5)
        {
            litPercentage += 50;
        }
        else
        {
            litPercentage += 35;
        }

        conditionsCheck();
    }

    // overload in case we want to add lighting boost modifiers
    public void litPercentageIncrease(Vector3 torchLocation, int modifier)
    {
        float distance = Vector3.Distance(this.transform.position, torchLocation);

        if (distance <= closeDistance)
        {
            litPercentage += 100;
        }
        else if (3 < distance && distance <= 5)
        {
            litPercentage += 50;
        }
        else
        {
            litPercentage += 35;
        }

        litPercentage += modifier;
        conditionsCheck();
    }

    public void litPercentageDecrease(Vector3 torchLocation)
    {
        float distance = Vector3.Distance(this.transform.position, torchLocation);

        if (distance <= closeDistance)
        {
            litPercentage -= 100;
        }
        else if (3 < distance && distance <= 5)
        {
            litPercentage -= 50;
        }
        else
        {
            litPercentage -= 35;
        }

        if (litPercentage < 0)
        {
            litPercentage = 0;
        }
        conditionsCheck();
    }

    // overload in case we want to add lighting boost modifiers
    public void litPercentageDecrease(Vector3 torchLocation, int modifier)
    {
        float distance = Vector3.Distance(this.transform.position, torchLocation);

        if (distance <= closeDistance)
        {
            litPercentage -= 100;
        }
        else if (3 < distance && distance <= 5)
        {
            litPercentage -= 50;
        }
        else
        {
            litPercentage -= 35;
        }

        litPercentage -= modifier;

        if (litPercentage < 0)
        {
            litPercentage = 0;
        }
        conditionsCheck();
    }
}
