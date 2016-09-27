using UnityEngine;
using System.Collections;

public class InputController : MonoBehaviour
{

    public Camera mainCamera;
    public float rayRange;
    public int abilityEquipped;
    private Ray actionRay;
    private RaycastHit hit;
    private GameObject hitTorch;

    // Use this for initialization
    void Start()
    {
        rayRange = 3;
        abilityEquipped = 1;
    }

    // Update is called once per frame
    void Update()
    {
        //create ray in center of the Camera
        actionRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));

        //cast highlight ray
        TorchHighlight();

        //check for mouse input
        MouseButtonInput(actionRay);
    }

    private void MouseButtonInput(Ray actRay)
    {
        if (Input.GetMouseButtonDown(0) && abilityEquipped == 1)
        {
            torchInstantiate.instantiateTorch(actRay, hit, rayRange, this.gameObject);
        }
    }

    private bool TorchHighlight()
    {
        if (Physics.Raycast(actionRay, out hit, rayRange))
        {
            if (hit.transform.gameObject.tag == "Torch")
            {
                if(hitTorch != null && hit.transform.gameObject != hitTorch){
                    highlightTorch.torchRemoveHighlight(hitTorch);
                }
                hitTorch = hit.transform.gameObject;
                highlightTorch.torchHighlight(hitTorch);
                //Outlined/Silhouette Only
                return true;
            }
            else if (hitTorch != null)
            {
                highlightTorch.torchRemoveHighlight(hitTorch);
                hitTorch = null;
            }
        }

        return false;
    }

    private void AbilitySwap()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            abilityEquipped = 1;
            rayRange = 3;
            abilityEquipped = 1;
        }
    }
}
