using UnityEngine;
using System.Collections;

public class colorIndicator : MonoBehaviour
{

    public Material tf;
    public Material tf2;

    private Material renderMaterial;

    // Use this for initialization
    void Start()
    {

    }

    public void setTorchFireMaterial(int OneOrTwo)
    {
        if (OneOrTwo == 1)
        {
            this.GetComponent<Renderer>().material = tf;
        }
        else if (OneOrTwo == 2)
        {
			this.GetComponent<Renderer>().material = tf2;
        }
    }

}
