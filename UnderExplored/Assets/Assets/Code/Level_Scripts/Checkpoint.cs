using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
	public int checkpointNumber;

    private GameObject roomManager;
	private GameObject player;
	private bool checkpointed;

	void Awake()
    {
		checkpointed = false;
		roomManager = GameObject.Find("RoomManager");
		player = GameObject.Find("Player");
    }

	public int getCheckPointNum(){
		return checkpointNumber;
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !checkpointed)
        {
			checkpointed = true;
			roomManager.GetComponent<Checkpoints>().setCheckpoint(this.transform);
			roomManager.GetComponent<Checkpoints>().setPlayerInventory(player.GetComponent<Inventory>());
        }
    }
}
