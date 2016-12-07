using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class fireChandelier : MonoBehaviour
{

    public GameObject fires;

	bool isLit;
	List<GameObject> nodes; // List of all nodes the torch affects

    // Use this for initialization
    void Start()
    {
		isLit = false;
		nodes = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("PlayerTorch") && !isLit)
        {
            isLit = true;
			//this.gameObject.tag = "Torch";
			fires.SetActive(true);
			this.GetComponent<SphereCollider>().enabled = false;
			StartCoroutine(turnOnCollider());
        }

		if (col.gameObject.CompareTag("Node") && isLit)
        {
			nodes.Add(col.gameObject);
            col.gameObject.GetComponent<LightNode>().litPercentageIncrease(this.transform.position);
        }
    }

	public void Extinguish()
	{
		isLit = false;
		fires.SetActive(false);

		if (nodes.Count > 0)
        {
            foreach (GameObject node in nodes)
            {
                node.GetComponent<LightNode>().litPercentageDecrease(this.transform.position);
            }
        }

		nodes = new List<GameObject>();
	}

	private IEnumerator turnOnCollider()
	{
		yield return new WaitForSeconds(1f);
		this.GetComponent<SphereCollider>().enabled = true;
	}
}
