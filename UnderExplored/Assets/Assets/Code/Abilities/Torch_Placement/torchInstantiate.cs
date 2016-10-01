using UnityEngine;
using System.Collections;

public class torchInstantiate : MonoBehaviour
{
    private static GameObject torchModel = (GameObject)Resources.Load("Torch_Fire", typeof(GameObject));
	public static LayerMask mask = 1 << LayerMask.NameToLayer("Wall");
    private static Vector3 torchSize = new Vector3(4f, 2f, 4f);

    public static void instantiateTorch(Ray ray, RaycastHit hit, float range, GameObject character)
    {
		Debug.Log("instTorch");
        if (Physics.Raycast(ray, out hit, range, mask))
        {
			Debug.Log("Got Here");
            GameObject placedTorch = Instantiate(torchModel, hit.point, Quaternion.identity) as GameObject;
            placedTorch.transform.localScale = torchSize;
            placedTorch.transform.rotation = Quaternion.FromToRotation(-character.transform.forward, hit.normal) * character.transform.rotation;
        }
    }

    public static void destroyTorch(RaycastHit hit){
        
    }
}
