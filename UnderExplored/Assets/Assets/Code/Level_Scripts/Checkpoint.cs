using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{
	public int checkpointNumber;
    private GameObject roomManager;

	void Awake()
    {
		roomManager = GameObject.Find("RoomManager");
    }

	public int getCheckPointNum(){
		return checkpointNumber;
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
			roomManager.GetComponent<Checkpoints>().setCheckpoint(this.transform);
        }
    }
}
