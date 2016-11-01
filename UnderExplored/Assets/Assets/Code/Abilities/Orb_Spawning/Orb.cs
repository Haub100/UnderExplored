using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Orb : MonoBehaviour
{

    [SerializeField]
    List<GameObject> nodes; // List of all nodes the torch affects
    private GameObject RoomManager;
    private GameObject activeDoor;
    private float lifeTime;
    private bool hasSetNodeIncrease;
    private bool badCast;
    private bool isForced;
    private bool ghostsSpawned;


    // Use this for initialization
    void Start()
    {
        RoomManager = GameObject.Find("RoomManager");
        activeDoor = RoomManager.GetComponent<RoomManager>().getActiveDoorFrame();
        ghostsSpawned = false;
        hasSetNodeIncrease = false;
        Physics.IgnoreLayerCollision(10, 11, true); //ignore player Collision
        Physics.IgnoreLayerCollision(10, 10, true); //ignore collision with itself
        Physics.IgnoreLayerCollision(10, 13, true); //ignore enemy collision
        lifeTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ghostsSpawned && badCast)
        {
            ghostsSpawned = true;
            activeDoor.GetComponent<EnemyHandler>().spawnGhosts();
        }

        //Waits until the orb comes to a complete stop before adding light percentage to the nodes
        if (this.gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero && 
            this.gameObject.GetComponent<Rigidbody>().isKinematic == false && lifeTime < 5)
        {
            //Debug.Log(lifeTime);
            this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        }

        if (this.gameObject.GetComponent<Rigidbody>().isKinematic == true && !hasSetNodeIncrease)
        {
            if (nodes.Count > 0)
            {
                foreach (GameObject node in nodes)
                {
                    node.GetComponent<LightNode>().litPercentageIncrease(this.transform.position);
                }
            }
            hasSetNodeIncrease = true;
        }

        //Subtracts from the lifetime of the orb until it hits zero
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            destroyO();
        }
    }
    public void setLifeTime(float timeToLive)
    {
        lifeTime = timeToLive;
    }

    public void isBadCast(bool isBad)
    {
        badCast = isBad;
    }
    public void setForced(bool forced)
    {
        isForced = forced;
    }

    // When the torch is detroyed it first subtracts its lit percentage from each node it affects based on its position relative to the node
    public void destroyO()
    {
        if (nodes.Count > 0)
        {
            foreach (GameObject node in nodes)
            {
                node.GetComponent<LightNode>().litPercentageDecrease(this.transform.position);
            }
        }
        //this.gameObject.GetComponent<Rigidbody>().isKinematic = false;
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Node"))
        {
            nodes.Add(col.gameObject);
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Node"))
        {
            nodes.Remove(col.gameObject);
            Debug.Log(nodes);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall") && isForced == true)
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
