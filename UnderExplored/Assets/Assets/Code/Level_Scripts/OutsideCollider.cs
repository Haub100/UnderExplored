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
        }
    }
}
