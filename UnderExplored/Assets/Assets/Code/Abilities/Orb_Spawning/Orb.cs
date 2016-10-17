using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Orb : MonoBehaviour
{

    [SerializeField]
    List<GameObject> nodes; // List of all nodes the torch affects
	float lifetime;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Node"))
        {
            nodes.Add(col.gameObject);
        }
    }

    // When the torch is detroyed it first subtracts its lit percentage from each node it affects based on its position relative to the node
    public void destroyT()
    {
        if (nodes.Count > 0)
        {
            foreach (GameObject node in nodes)
            {
                node.GetComponent<LightNode>().litPercentageDecrease(this.transform.position);
            }
        }
        Destroy(this.transform.parent.gameObject);
    }
}
