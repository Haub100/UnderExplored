using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class ProgressionIndicator : MonoBehaviour
{

    public GameObject objectWithNController; //An Object with a node controller
    public List<GameObject> progressionDots;

    private Material inactive;
    private Material active;
    private int totalNodeCount;
    private int totalPDots;
    private int litCount;
    private bool isProgressed;


    // Use this for initialization
    void Start()
    {
        active = (Material)Resources.Load("Materials/ProgressionActive", typeof(Material));
        inactive = (Material)Resources.Load("Materials/ProgressionInactive", typeof(Material));
        totalNodeCount = objectWithNController.GetComponent<NodeController>().Nodes.Count;
        totalPDots = progressionDots.Count;
        isProgressed = false;
    }

    // Update is called once per frame
    void Update()
    {
        litCount = objectWithNController.GetComponent<NodeController>().getLitCount();
        updateProgression();
    }

	public bool getIsProgressed(){
		return isProgressed;
	}

    private void updateProgression()
    {
        decimal percentageNodesLit = decimal.Divide(litCount, totalNodeCount);
        double dotsDouble = (double)(totalPDots * percentageNodesLit);
        double dotsToLight = Math.Round(dotsDouble, 0, MidpointRounding.AwayFromZero);
        int dotLight = (int)dotsToLight;

        int i = 0;
        if (dotLight > 0)
        {
            while (i < dotLight)
            {
                progressionDots[i].GetComponent<Renderer>().material = active;
                i++;
            }
        }
        while (i < totalPDots)
        {
            progressionDots[i].GetComponent<Renderer>().material = inactive;
            i++;
        }

        if (dotLight == 10)
        {
            isProgressed = true;
        }
        else
        {
			isProgressed = false;
        }
    }
}
