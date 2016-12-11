using UnityEngine;
using System.Collections;

public class cave_InKill : MonoBehaviour {

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
    }

	    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            player.GetComponent<PlayerHealth>().TakeDamage(100);
        }
    }
}
