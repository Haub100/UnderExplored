using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
public class ProgressionIndicator : MonoBehaviour
{

    public GameObject objectWithNController; //An Object with a node controller
    public List<GameObject> progressionDots;
    public GameObject finalDot;

    private float percentToProgress;
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

        totalPDots = progressionDots.Count;
        isProgressed = false;

    }

    // Update is called once per frame
    void Update()
    {
        totalNodeCount = objectWithNController.GetComponent<NodeController>().Nodes.Count;
        litCount = objectWithNController.GetComponent<NodeController>().getLitCount();
        percentToProgress = objectWithNController.GetComponent<NodeController>().getPercentToProgress();
        updateProgression();
    }

    public void setObjectWithNController(GameObject ObjWithNController)
    {
        objectWithNController = ObjWithNController;
    }

    public bool getIsProgressed()
    {
        return isProgressed;
    }

    private void updateProgression()
    {
        decimal decimalPercent = decimal.Parse(percentToProgress.ToString()); //needed because we need to convert the float to a decimal in order to divide
        decimal nodesToLight = (totalNodeCount * decimalPercent);
        decimal percentageNodesLit = decimal.Divide(litCount, nodesToLight);

        //This prevents an out of range exception
        if (percentageNodesLit > 1)
        {
            percentageNodesLit = 1;
        }

        double dotsDouble = (double)(totalPDots * percentageNodesLit);
        double dotsToLight = Math.Round(dotsDouble, 0, MidpointRounding.AwayFromZero);
        int dotLight = (int)dotsToLight;


        int i = 0;
        if (dotLight > 0)
        {
            while (i < dotLight)
            {
                if (progressionDots[i].CompareTag("UI_Prog"))
                {
                    progressionDots[i].SetActive(false);
                }
                else
                {
                    progressionDots[i].GetComponent<Renderer>().material = active;
                }
                i++;
            }
        }
        while (i < totalPDots)
        {
            if (progressionDots[i].CompareTag("UI_Prog"))
            {
                progressionDots[i].SetActive(true);
            }
            else
            {
                progressionDots[i].GetComponent<Renderer>().material = inactive;
            }
            i++;
        }

        //If a room is fully lit we light up the final dot in the progression indicator
        if (objectWithNController.GetComponent<NodeController>().getIsFullyLit())
        {
            if (finalDot.CompareTag("UI_Prog"))
            {
                finalDot.SetActive(false);
            }
            else
            {
                finalDot.GetComponent<Renderer>().material = active;
            }
        }
        else
        {
            if (finalDot.CompareTag("UI_Prog"))
            {
                finalDot.SetActive(true);
            }
            else
            {
                finalDot.GetComponent<Renderer>().material = inactive;
            }
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
