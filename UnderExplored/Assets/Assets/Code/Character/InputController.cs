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
        hitTorch = null;
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
        //If a torch is highlighted a mouse click will delete it otherwise one will be instantiated
        if (Input.GetMouseButtonDown(0) && abilityEquipped == 1 && hitTorch == null)
        {
            Torch.instantiateT(actRay, hit, rayRange, this.gameObject);
        }
        else if(Input.GetMouseButtonDown(0) && abilityEquipped == 1){
            Torch.removeT(hitTorch);
            StartCoroutine(WaitOneFrameT(hitTorch));
        }
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

    //needed because before we destroy a torch we have to wait one frame
    //this allows OnTriggerExit to be called in a node
    IEnumerator WaitOneFrameT(GameObject torch){
        yield return 0;
        Torch.destroyT(torch);
    }
}
