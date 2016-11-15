using UnityEngine;
using System.Collections;

public class Ghost : MonoBehaviour
{

    Transform player;
    NavMeshAgent nav;
    Transform destination;

    void Awake()
    {
        destination = player = GameObject.Find("Player").transform;
        nav = GetComponent<NavMeshAgent>();
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        nav.SetDestination(destination.position);
    }

    void OnCollisionStay(Collision col)
    {
		float distance = Vector3.Distance(destination.position, this.transform.position);

        if (col.gameObject.layer == LayerMask.NameToLayer("EnemyWall") && distance > 2f )
        {
			destination = this.transform;
        }
		else{
			destination = player;
		}
    }

    public void SetDestination (Transform dest){
        destination = dest;
    }
}
