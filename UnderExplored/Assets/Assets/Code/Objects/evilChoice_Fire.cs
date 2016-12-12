using UnityEngine;
using System.Collections;

public class evilChoice_Fire : MonoBehaviour
{

    public GameObject fireWall;

    // Use this for initialization
    void Start()
    {
        fireWall.SetActive(false);
    }

    public void activate()
    {
        fireWall.SetActive(true);
    }

    public void checkPointReset()
    {
		fireWall.SetActive(false);
    }

}
