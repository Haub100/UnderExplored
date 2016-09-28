using UnityEngine;
using System.Collections;

public class Torch : MonoBehaviour
{
    private static GameObject torchModel = (GameObject)Resources.Load("Torch_Fire", typeof(GameObject));
    private static GameObject silhouetteShaderHolder = (GameObject)Resources.Load("SilhouetteShaderHolder", typeof(GameObject));
    private static GameObject normalTorchShaderHolder = (GameObject)Resources.Load("NormalTorchShaderHolder", typeof(GameObject));
    public static LayerMask mask = 1 << LayerMask.NameToLayer("Wall");
    private static Vector3 torchSize = new Vector3(4f, 2f, 4f);
	public static Torch instance;
	
    public static void instantiateT(Ray ray, RaycastHit hit, float range, GameObject character)
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

    public static void removeT(GameObject torch)
    {
		torch.transform.parent.gameObject.transform.position = new Vector3(1000f,1000f,1000f);
    }

	public static void destroyT(GameObject torch){
		Destroy(torch.transform.parent.gameObject);
	}

    public static GameObject highlightT(Ray actionRay, float rayRange, GameObject hitTorch)
    {
        RaycastHit hit;

        if (Physics.Raycast(actionRay, out hit, rayRange))
        {
            if (hit.transform.gameObject.tag == "Torch")
            {
                if (hitTorch != null && hit.transform.gameObject != hitTorch)
                {
                    hitTorch.GetComponent<Renderer>().material.shader = normalTorchShaderHolder.GetComponent<Renderer>().sharedMaterial.shader;
                }
                hitTorch = hit.transform.gameObject;
                hitTorch.GetComponent<Renderer>().material.shader = silhouetteShaderHolder.GetComponent<Renderer>().sharedMaterial.shader;
                return hitTorch;
            }
            else if (hitTorch != null)
            {
                hitTorch.GetComponent<Renderer>().material.shader = normalTorchShaderHolder.GetComponent<Renderer>().sharedMaterial.shader;
                hitTorch = null;
            }
        }
        return null;
    }
}
