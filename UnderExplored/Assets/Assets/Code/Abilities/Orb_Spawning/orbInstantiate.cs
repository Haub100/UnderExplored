using UnityEngine;
using System.Collections;

public class orbInstantiate : MonoBehaviour
{
    private static bool trueBool = true;
    private static GameObject orbModel = (GameObject)Resources.Load("Prefabs/Orb", typeof(GameObject));

    public static void instantiateOrb(Vector3 location, Vector3 direction, float force, bool badCast)
    {
        Debug.Log(force);
        GameObject orb = Instantiate(orbModel, location, Quaternion.identity) as GameObject;
        orb.GetComponent<Orb>().isBadCast(badCast);
		orb.GetComponent<Rigidbody>().AddForce(direction * force);
        orb.GetComponent<Orb>().setForced(trueBool);
        
    }
}
