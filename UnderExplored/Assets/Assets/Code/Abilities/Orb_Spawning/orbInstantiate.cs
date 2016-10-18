using UnityEngine;
using System.Collections;

public class orbInstantiate : MonoBehaviour
{

    private static GameObject orbModel = (GameObject)Resources.Load("Prefabs/Orb", typeof(GameObject));

    public static void instantiateOrb(Vector3 location, Vector3 direction, float force)
    {
        Debug.Log("instOrb");

        Debug.Log("Got Here");
        GameObject orb = Instantiate(orbModel, location, Quaternion.identity) as GameObject;
        //orb.GetComponent<Orb>().setLifeTime(5f);
		orb.GetComponent<Rigidbody>().AddForce(direction * force);
    }
}
