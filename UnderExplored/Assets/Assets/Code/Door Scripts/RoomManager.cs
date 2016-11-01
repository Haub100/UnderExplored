using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoomManager : MonoBehaviour
{
    public GameObject activeDoorFrame; //doorframe the player is currently trying to get through

    //private List<GameObject> destroyableObjects;

    // Use this for initialization
    void Start()
    {
        Physics.IgnoreLayerCollision(12, 11, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setActiveDoor(GameObject doorFrame)
    {
        activeDoorFrame = doorFrame;
    }

    public void despawnObjects()
    {
        GameObject[] destroyableObjects = GameObject.FindGameObjectsWithTag("Torch");
        if (destroyableObjects.Length > 0)
        {
            foreach (GameObject obj in destroyableObjects)
            {
                obj.GetComponent<Torch>().destroyT();
            }
        }
    }

    public GameObject getActiveDoorFrame()
    {
        return activeDoorFrame;
    }

    public void addDestroyObject(GameObject obj)
    {
        //destroyableObjects.Add(obj);
    }

    public void removeDestroyObject(GameObject obj)
    {
        //destroyableObjects.Remove(obj);
    }
}
