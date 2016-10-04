using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Torch : MonoBehaviour
{

    private static GameObject silhouetteShaderHolder;
    private static GameObject normalTorchShaderHolder;

    [SerializeField]
    List<GameObject> nodes; // List of all nodes the torch affects

    void Awake(){
        silhouetteShaderHolder = (GameObject)Resources.Load("SilhouetteShaderHolder", typeof(GameObject));
        normalTorchShaderHolder = (GameObject)Resources.Load("NormalTorchShaderHolder", typeof(GameObject));
        
    }

    public void destroyT()
    {
        if(nodes.Count > 0){
            foreach(GameObject node in nodes){
                node.GetComponent<LightNode>().litPercentageDecrease(this.transform.position);
            }
        }
        Destroy(this.transform.parent.gameObject);
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

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Node"))
        {
            nodes.Add(col.gameObject);
        }
    }
}
