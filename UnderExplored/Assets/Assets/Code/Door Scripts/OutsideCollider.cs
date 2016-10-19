using UnityEngine;
using System.Collections;

public class OutsideCollider : MonoBehaviour
{
    public GameObject nextDoorFrame; //Can be null
    private GameObject DoorManager;
    // Use this for initialization
    void Start()
    {
        DoorManager = GameObject.Find("DoorManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (nextDoorFrame != null)
            {
                DoorManager.GetComponent<DoorManager>().setActiveDoor(nextDoorFrame);
            }
        }
    }
}
