using UnityEngine;
using System.Collections;

public class highlightTorch : MonoBehaviour {

	static GameObject silhouetteShaderHolder = (GameObject)Resources.Load("SilhouetteShaderHolder", typeof(GameObject));
	private int hello;
	static GameObject normalTorchShaderHolder = (GameObject)Resources.Load("NormalTorchShaderHolder", typeof(GameObject));

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void torchHighlight (GameObject torch){
		torch.GetComponent<Renderer>().material.shader = silhouetteShaderHolder.GetComponent<Renderer>().sharedMaterial.shader;
	}

	public static void torchRemoveHighlight(GameObject torch){
		torch.GetComponent<Renderer>().material.shader = normalTorchShaderHolder.GetComponent<Renderer>().sharedMaterial.shader;
	}
}
