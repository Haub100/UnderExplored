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
    private static GameObject torchModel;
    public LayerMask wallMask;
    private Vector3 torchSize = new Vector3(4f, 2f, 4f);
    private int torches;

    // Use this for initialization
    void Start()
    {
        hitTorch = null;
        rayRange = 3;
        abilityEquipped = 1;
        wallMask = 1 << LayerMask.NameToLayer("Wall");
        torchModel = (GameObject)Resources.Load("Torch_Fire", typeof(GameObject));
    }

    // Update is called once per frame
    void Update()
    {
        //create ray in center of the Camera
        actionRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        torches = GetComponent<Inventory>().getTorches();

        //cast highlight ray
        if (abilityEquipped == 1)
        {
            TorchHighlight();
        }


        //check for mouse input
        MouseButtonInput(actionRay);
    }

    private void MouseButtonInput(Ray actRay)
    {
        //If a torch is highlighted a mouse click will delete it otherwise one will be instantiated
        if (Input.GetMouseButtonDown(0) && abilityEquipped == 1 && hitTorch == null && torches > 0)
        {
            if (instantiateTorch())
            {
                this.GetComponent<Inventory>().removeTorches(1);
            }
        }
        else if (Input.GetMouseButtonDown(0) && abilityEquipped == 1 && hitTorch != null)
        {
            this.GetComponent<Inventory>().addTorches(1);
            hitTorch.GetComponent<Torch>().destroyT();
        }
    }

    private bool instantiateTorch()
    {
        if (Physics.Raycast(actionRay, out hit, rayRange, wallMask))
        {
            GameObject placedTorch = Instantiate(torchModel, hit.point, Quaternion.identity) as GameObject;
            placedTorch.transform.localScale = torchSize;
            placedTorch.transform.rotation = Quaternion.FromToRotation(-this.transform.forward, hit.normal) * this.transform.rotation;
            return true;
        }
        return false;
    }

    private bool TorchHighlight()
    {
        hitTorch = Torch.highlightT(actionRay, rayRange, hitTorch);

        if (hitTorch == null)
        {
            return false;
        }
        else
        {
            return true;
        }
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
