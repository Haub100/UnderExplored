using UnityEngine;
using System.Collections;

public class ThiefGhost : MonoBehaviour
{

    public GameObject objectWithNController;

    private GameObject player;
    public GameObject body;
    public GameObject effect;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        this.body.SetActive(false);
        this.effect.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (objectWithNController.GetComponent<NodeController>().getIsLitEnough()
        && player.GetComponent<Inventory>().getTorches() > 0)
        {
            this.body.SetActive(true);
            this.effect.SetActive(true);
        }
        else if (objectWithNController.GetComponent<NodeController>().getIsLitEnough()
        && player.GetComponent<Inventory>().getTorches() == 0)
        {
            this.body.SetActive(false);
            this.effect.SetActive(false);
        }
		else if(!objectWithNController.GetComponent<NodeController>().getIsLitEnough())
		{
			this.body.SetActive(false);
            this.effect.SetActive(false);
		}
    }
}
