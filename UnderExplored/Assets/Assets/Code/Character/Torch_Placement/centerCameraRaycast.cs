using UnityEngine;
using System.Collections;

public class centerCameraRaycast : MonoBehaviour
{

    public Camera mainCamera;
	public GameObject torch;
	public Vector3 torchSize;

    void Start()
    {
    }

    void Update()
    {
        Ray ray = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        RaycastHit hit;
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit, 1f))
            {
                print(hit.point);
				print("normal: " + hit.normal);
                print("I'm looking at " + hit.transform.name);
				Quaternion rot = Quaternion.Euler(hit.normal.x, hit.normal.y, hit.normal.z);
				GameObject placedTorch = Instantiate(torch, hit.point, rot) as GameObject;
				placedTorch.transform.localScale = torchSize;
            }
            else
                print("I'm looking at nothing!");
        }
    }
}
