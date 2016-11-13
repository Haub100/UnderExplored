using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyHandler : MonoBehaviour {
	// The EnemyHandler class is designed to keep track of enemy spawn points within a given room.
	// Enemies are not designed to leave the particular room they spawn in 

	public List<Transform> ghostSpawnLocations;
	private GameObject GhostModel;

	// Use this for initialization
	void Start () {
		GhostModel = (GameObject)Resources.Load("Prefabs/Enemies/Ghost", typeof(GameObject));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void spawnGhosts(){
		if(ghostSpawnLocations.Count > 0){
			foreach(Transform location in ghostSpawnLocations){
				Instantiate(GhostModel, location.position, Quaternion.identity);
			}
		}
	}
}
