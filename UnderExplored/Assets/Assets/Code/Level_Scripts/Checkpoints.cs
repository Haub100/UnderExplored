using UnityEngine;
using System.Collections;

public class Checkpoints : MonoBehaviour
{
    public int startingCheckpoint;
    private Checkpoint checkpoint;
    private Inventory playerInventory;
    private GameObject player;
    private GameObject[] checkpointList;
    private GameObject gameManager;

    // Use this for initialization
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        if(gameManager != null) //if statement for testing puposes can be removed in build
        {
            startingCheckpoint = gameManager.GetComponent<GameManager>().getStartingLevel();
        }
        
        player = GameObject.Find("Player");
        playerInventory = this.GetComponent<Inventory>();
        //checkpoint = GameObject.Find("DungeonStart").transform;
        checkpointList = GameObject.FindGameObjectsWithTag("Checkpoint");
        setCheckpoint(startingCheckpoint);
        if (startingCheckpoint == 1)
        {
            player.GetComponent<Inventory>().addTorches(2);
            foreach (GameObject chkpt in checkpointList)
            {
                if (chkpt.GetComponent<Checkpoint>().getCheckPointNum() == 1)
                {
                    chkpt.GetComponent<Checkpoint>().setPoints(15);
                }
            }
        }
        else if (startingCheckpoint == 2)
        {
            foreach (GameObject chkpt in checkpointList)
            {
                if (chkpt.GetComponent<Checkpoint>().getCheckPointNum() == 2)
                {
                    chkpt.GetComponent<Checkpoint>().setPoints(35);
                }
            }
        }
    }

    void Start()
    {
        playerInventory.setInventory(player.GetComponent<Inventory>());
    }

    //Getters & Setters
    public Checkpoint getCheckpoint()
    {
        return checkpoint;
    }

    public void setCheckpoint(Checkpoint newCheckpoint)
    {
        checkpoint = newCheckpoint;
    }

    public void setCheckpoint(int checkpointNum)
    {
        foreach (GameObject chkpt in checkpointList)
        {
            if (chkpt.GetComponent<Checkpoint>().getCheckPointNum() == checkpointNum)
            {
                checkpoint = chkpt.GetComponent<Checkpoint>();
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
        player.transform.position = checkpoint.transform.position;
    }
}
