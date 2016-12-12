using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
    public int checkpointNumber;
	public GameObject nextDoorFrame; //The doorframe that the player next goes through after the checkpoint

    private GameObject roomManager;
    private GameObject player;
    private int points; //holds the number of points the playe has when they reach a checkpoint
    private bool checkpointed;

    void Awake()
    {
        checkpointed = false;
        roomManager = GameObject.Find("RoomManager");
        player = GameObject.Find("Player");
        points = 0;
    }

	public void setPoints(int newPoints)
	{
		points = newPoints;
	}

    public int getCheckPointNum()
    {
        return checkpointNumber;
    }

    public int getPoints()
    {
		return points;
    }
	
	public GameObject getNextDoorFrame(){
		return nextDoorFrame;
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !checkpointed)
        {
            checkpointed = true;
            roomManager.GetComponent<Checkpoints>().setCheckpoint(this);
            roomManager.GetComponent<Checkpoints>().setPlayerInventory(player.GetComponent<Inventory>());
			if(points == 0)
			{
				points = roomManager.GetComponent<RoomManager>().getPoints();
			}
            
			roomManager.GetComponent<RoomManager>().resetDoorFramesSinceLastCheckpoint();
        }
    }
}
