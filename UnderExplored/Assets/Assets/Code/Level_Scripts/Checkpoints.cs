using UnityEngine;
using System.Collections;

public class Checkpoints : MonoBehaviour
{
    public int startingCheckpoint;

    private Transform checkpoint;
    private Inventory playerInventory;
    private GameObject player;
    private GameObject[] checkpointList;

    // Use this for initialization
    void Awake()
    {
        player = GameObject.Find("Player");
        playerInventory = this.GetComponent<Inventory>();
        //checkpoint = GameObject.Find("DungeonStart").transform;
		checkpointList = GameObject.FindGameObjectsWithTag("Checkpoint");
		setCheckpoint(startingCheckpoint);
        
    }

    void Start()
    {
        playerInventory.setInventory(player.GetComponent<Inventory>());
    }

    //Getters & Setters
    public Transform getCheckpoint()
    {
        return checkpoint;
    }

    public void setCheckpoint(Transform newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }

    public void setCheckpoint(int checkpointNum)
    {
        foreach (GameObject chkpt in checkpointList)
        {
            if (chkpt.GetComponent<Checkpoint>().getCheckPointNum() == checkpointNum)
            {
                checkpoint = chkpt.transform;
            }
        }
    }

    public void setPlayerInventory(Inventory currentInventory)
    {
        playerInventory.setInventory(currentInventory);
    }

    // This method is used when the player needs to spawn on a checkpoint. It reloads a previous Inventory
    public void loadInventory()
    {
        player.GetComponent<Inventory>().setInventory(playerInventory);
    }

    // This method is called when the player moves back to the previous checkpoint;
    public void loadCheckpoint()
    {
        player.transform.position = checkpoint.position;
    }
}
