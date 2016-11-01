using UnityEngine;
using System.Collections;

public class OutsideCollider : MonoBehaviour
{
    public GameObject nextDoorFrame; //Can be null
    public GameObject playerBlock;
    private GameObject RoomManager;
    private bool isdespawned = false;
    // Use this for initialization
    void Start()
    {
        RoomManager = GameObject.Find("RoomManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player") && !isdespawned)
        {
            isdespawned = true;
            RoomManager.GetComponent<RoomManager>().despawnObjects();
            playerBlock.SetActive(true);
            if (nextDoorFrame != null)
            {
                RoomManager.GetComponent<RoomManager>().setActiveDoor(nextDoorFrame);
            }
        }
    }
}
