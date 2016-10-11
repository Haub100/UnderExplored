using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
    private LayerMask actionItems;
    private Vector3 torchSize = new Vector3(4f, 2f, 4f);
    private int torches;

    //Variables for UI interaction
    private Text actionUIText;
    private GameObject actionIcon;
    private string actionString;

    // Use this for initialization
    void Start()
    {
        hitTorch = null;
        rayRange = 3;
        abilityEquipped = 1;
        wallMask = 1 << LayerMask.NameToLayer("Wall");
        actionItems = 1 << LayerMask.NameToLayer("Action_Items");
        torchModel = (GameObject)Resources.Load("Torch_Fire", typeof(GameObject));
        actionString = "Press 'E'";
        GameObject textOBJ = GameObject.Find("Interact Text");
        actionUIText = textOBJ.GetComponent<Text>();
        actionUIText.text = "";
        actionIcon = GameObject.Find("Interact Icon");
        actionIcon.SetActive(false);
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

        //E-button Raycast
        eButton();

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

    private void eButton()
    {
        if (Physics.Raycast(actionRay, out hit, rayRange, actionItems))
        {
            actionUIText.text = actionString;
            actionIcon.SetActive(true);
        }
        else
        {
            actionUIText.text = "";
            actionIcon.SetActive(false);
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
