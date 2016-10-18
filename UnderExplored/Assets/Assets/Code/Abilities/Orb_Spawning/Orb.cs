﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Orb : MonoBehaviour
{

    [SerializeField]
    List<GameObject> nodes; // List of all nodes the torch affects
    private float lifeTime;
    private bool hasSetNodeIncrease;

    // Use this for initialization
    void Start()
    {
        hasSetNodeIncrease = false;
        Physics.IgnoreLayerCollision(10, 11, true);
        Physics.IgnoreLayerCollision(10, 10, true);
        lifeTime = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        //Waits until the orb comes to a complete stop before adding light percentage to the nodes
        if (this.gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero && hasSetNodeIncrease == false)
        {
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
        if (col.gameObject.layer == LayerMask.NameToLayer("Wall"))
        {
            this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
