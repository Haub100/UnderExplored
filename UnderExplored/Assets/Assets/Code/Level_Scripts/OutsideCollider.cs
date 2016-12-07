using UnityEngine;
using System.Collections;

public class OutsideCollider : MonoBehaviour
{
    public GameObject nextDoorFrame; //Can be null
    public GameObject playerBlock;
    private GameObject RoomManager;
    private GameObject progressionHud;
    private bool isdespawned = false;
    // Use this for initialization
    void Start()
    {
        RoomManager = GameObject.Find("RoomManager");
        progressionHud = GameObject.Find("ProgressionHUD");
    }

    // Update is called once per frame
    void Update()
    {
        if (!this.GetComponentInParent<NodeController>().getIsLitEnough())
        {
            playerBlock.SetActive(true);
        }
        else
        {
            playerBlock.SetActive(false);
        }
    }

    public void setIsDespawned(bool isDespanwed)
    {
        isdespawned = isDespanwed;
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
                progressionHud.GetComponent<ProgressionIndicator>().setObjectWithNController(nextDoorFrame);
            }

            //Add the door frame to the room manager's door frame's since last checkpoint
            //This will allow the room manager to reset the isDespanwed boolean
            RoomManager.GetComponent<RoomManager>().addDoorFrameSinceCheckpoint(this.transform.parent.gameObject);

            //adds the proper points to the room manager
            if (this.GetComponentInParent<NodeController>().getIsFullyLit())
            {
                RoomManager.GetComponent<RoomManager>().addPoints(10);
            }
            else
            {
                RoomManager.GetComponentInParent<RoomManager>().addPoints(5);
            }
        }
    }
}
