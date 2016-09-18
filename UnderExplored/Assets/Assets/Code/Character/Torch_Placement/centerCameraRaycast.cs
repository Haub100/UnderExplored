using UnityEngine;
using System.Collections;

public class centerCameraRaycast : MonoBehaviour
{

    public Camera mainCamera;
	public GameObject torch;
	public Vector3 torchSize;
	public LayerMask mask = 0;
	public float range;

    void Start()
    {
    }

    void Update()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, range, mask))
            {
                print(hit.point);
				print("normal: " + hit.normal);
                print("I'm looking at " + hit.transform.name);
				//Quaternion rot = Quaternion.Euler(hit.normal.x, hit.normal.y, hit.normal.z);
				GameObject placedTorch = Instantiate(torch, hit.point, Quaternion.identity) as GameObject;
				placedTorch.transform.localScale = torchSize;
				placedTorch.transform.rotation = Quaternion.FromToRotation (-transform.forward, hit.normal) * transform.rotation;

				//RaycastHit[] RaycastHit = Physics.RaycastAll(ray, 2f);
				/*Vector3 location = wallPlacementLocation(RaycastHit);
				if(location == new Vector3(0f,0f,0f)){
					GameObject placedTorch = Instantiate(torch, location, rot) as GameObject;
					placedTorch.transform.localScale = torchSize;
				}*/

            }
            else
                print("I'm looking at nothing!");
        }
    }

	private Vector3 wallPlacementLocation(RaycastHit[] hits){
		Vector3 returnVec = new Vector3(0f,0f,0f);

		foreach (RaycastHit hit in hits){
			if(hit.transform.tag == "Wall"){
				Debug.Log("Got here" + hit.point);
				returnVec = hit.point;
			}else{
				continue;
			}
		}
		return returnVec;
	}
}
