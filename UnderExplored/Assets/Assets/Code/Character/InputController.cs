using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InputController : MonoBehaviour
{
    //Public Variables
    public Camera mainCamera;
    public LayerMask wallMask;
    public float rayRange;
    public int abilityEquipped;
    public GameObject playerTorch;

    //Private Variables
    private static GameObject torchModel; //torch model to be instantiated
    private GameObject hitTorch; //torch that is highlighted if no highlight it is null
    private LayerMask actionItems; //all objects that can be used with the action 'E' key
    private Ray actionRay; //Ray that comes out from the middle of the player camera
    private RaycastHit hit;
    private Vector3 torchSize = new Vector3(4f, 2f, 4f);
    private Vector3 instantiateLocation; //the location a spell is instantiated
    private Vector3 instantateDirection; //the direction a spell travels when cast
    private bool isCharging = false; //used for charging abilities
    private bool inCooldown = false;
    private float force; //used for casting spells a specific distance
    private float heightWidth; //determines the width and the height of the orb reticle
    private int torches;
    private float countdown; //countdown timer used for UI cooldowns

    //Variables for UI interaction
    private Text actionUIText;
    private Text infoUIText;
    private Text popUpText;
    private Text OrbTimerText;
    private GameObject CrosshairUI;
    private GameObject OrbReticleUI;
    private GameObject GreenZoneUI;
    private GameObject actionIcon;
    private GameObject TorchUI;
    private GameObject OrbUI;
    private GameObject OrbTimer;
    private Sprite OrbUISprite;
    private Sprite OrbUISelectedSprite;
    private Sprite OrbUIGreyscaleSprite;
    private Sprite TorchUISprite;
    private Sprite TorchUISelectedSprite;
    private string actionString;
    private bool inPopCoroutine = false;


    // Use this for initialization
    void Start()
    {
        hitTorch = null;
        rayRange = 3;
        abilityEquipped = 1;
        wallMask = 1 << LayerMask.NameToLayer("Wall");
        actionItems = 1 << LayerMask.NameToLayer("Action_Items");
        torchModel = (GameObject)Resources.Load("Torch_Fire", typeof(GameObject));
        force = 600;
        //playerTorch.SetActive(false);

        //UI assignments
        actionString = "Press 'E'";
        actionUIText = GameObject.Find("Interact Text").GetComponent<Text>();
        infoUIText = GameObject.Find("InfoText").GetComponent<Text>();
        popUpText = GameObject.Find("PopUpText").GetComponent<Text>();
        OrbTimerText = GameObject.Find("OrbTimer").GetComponent<Text>();
        TorchUI = GameObject.Find("TorchUI");
        OrbUI = GameObject.Find("OrbUI");
        OrbTimer = GameObject.Find("OrbTimer");
        CrosshairUI = GameObject.Find("Crosshair");
        OrbReticleUI = GameObject.Find("OrbReticle");
        GreenZoneUI = GameObject.Find("GreenZoneUI");
        OrbUISprite = (Sprite)Resources.Load("Textures/OrbUI", typeof(Sprite));
        OrbUISelectedSprite = (Sprite)Resources.Load("Textures/OrbUISelected", typeof(Sprite));
        OrbUIGreyscaleSprite = (Sprite)Resources.Load("Textures/OrbUIGrey", typeof(Sprite));
        TorchUISprite = (Sprite)Resources.Load("Textures/TorchUI", typeof(Sprite));
        TorchUISelectedSprite = (Sprite)Resources.Load("Textures/TorchUISelected", typeof(Sprite));
        TorchUI.GetComponent<Image>().sprite = TorchUISelectedSprite;
        OrbUI.GetComponent<Image>().sprite = OrbUISprite;
        OrbReticleUI.SetActive(false);
        GreenZoneUI.SetActive(false);
        actionUIText.text = "";
        actionIcon = GameObject.Find("Interact Icon");
        actionIcon.SetActive(false);
        OrbTimer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //create ray in center of the Camera
        actionRay = mainCamera.ViewportPointToRay(new Vector3(0.5F, 0.5F, 0));
        torches = GetComponent<Inventory>().getTorches();
        //instantateLocation in the center of the Camera
        instantiateLocation = mainCamera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1f));
        //instantiateDirection where the camera is facing
        instantateDirection = mainCamera.transform.forward;

        //cast highlight ray
        if (abilityEquipped == 1)
        {
            if(torches == 0){
                playerTorch.SetActive(false);
            }
            TorchHighlight();
        }

        //E-button Raycast
        eButton();

        //Cooldown for the orb ability
        if (inCooldown)
        {
            OrbCooldown();
        }

        //check for mouse input
        MouseButtonInput();

        //check for AbilitySwap
        AbilitySwap();
    }

    private void MouseButtonInput()
    {

        //If a torch is highlighted a mouse click will delete it otherwise one will be instantiated
        if (abilityEquipped == 1)
        {
            if (Input.GetMouseButtonDown(0) && hitTorch == null && torches > 0)
            {
                if (instantiateTorch())
                {
                    this.GetComponent<Inventory>().removeTorches(1);
                    if(this.GetComponent<Inventory>().getTorches() == 0){
                        playerTorch.SetActive(false);
                    }
                }
            }
            else if (Input.GetMouseButtonDown(0) && hitTorch != null)
            {
                if (!this.GetComponent<Inventory>().isFull())
                {
                    this.GetComponent<Inventory>().addTorches(1);
                    hitTorch.GetComponent<Torch>().destroyT();
                    playerTorch.SetActive(true);
                }
                else if (!inPopCoroutine)
                {
                    StartCoroutine(popText("INVETORY FULL"));
                }
            }
        }

        //If ability 2 is equipped it will spawn a light orb based with a distance based on a charge
        if (abilityEquipped == 2)
        {
            //Starts the charging process
            if (Input.GetMouseButtonDown(0) && !inCooldown)
            {
                isCharging = true;
                GreenZoneUI.SetActive(true);
            }
            else if (Input.GetMouseButton(0) && isCharging == true) //If the player hasn't cancelled the charge this code runs and charges the orb spell
            {
                float floor = 100f;
                float ceiling = 500f;
                heightWidth = Mathf.PingPong(Time.time * 1000f, ceiling - floor);
                heightWidth += 100;
                OrbReticleUI.GetComponent<RectTransform>().sizeDelta = new Vector2(heightWidth, heightWidth);
            }
            else if (Input.GetMouseButtonUp(0) && isCharging == true) //Casts orb spell if the player hasn't cancelled
            {
                bool badcast;
                if (heightWidth > 400)
                {
                    badcast = false;
                }
                else
                {
                    badcast = true;
                }
                orbInstantiate.instantiateOrb(instantiateLocation, instantateDirection, force, badcast);
                OrbReticleUI.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
                OrbUI.GetComponent<Image>().sprite = OrbUIGreyscaleSprite;
                GreenZoneUI.SetActive(false);
                inCooldown = true;
                isCharging = false;
                OrbTimer.SetActive(true);
                countdown = 5f;
            }

            //Cancels the charging process
            if (Input.GetMouseButtonDown(1))
            {
                isCharging = false;
                GreenZoneUI.SetActive(false);
                OrbReticleUI.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
            }
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
        // Checks whether or not the player is looking at an interactible object
        if (Physics.Raycast(actionRay, out hit, rayRange, actionItems))
        {
            actionUIText.text = actionString;
            actionIcon.SetActive(true);

            //If if the interactible object gives the player more torches;
            if (hit.transform.gameObject.tag == "TorchSource")
            {

                //Displays how many torches are available to pick up
                infoUIText.text = "Torches: " + hit.transform.gameObject.GetComponent<TorchSource>().torchCount;

                //Adds torches to inventory by getting torches needed from the Torch Source
                if (Input.GetKeyDown(KeyCode.E))
                {
                    if (this.GetComponent<Inventory>().torchesNeeded() > 0 && hit.transform.gameObject.GetComponent<TorchSource>().getTorchCount() > 0)
                    {
                        this.GetComponent<Inventory>()
                        .addTorches(hit.transform.gameObject.GetComponent<TorchSource>().
                        takeTorches(this.GetComponent<Inventory>().torchesNeeded()));
                        playerTorch.SetActive(true);
                    }
                    else if (this.GetComponent<Inventory>().torchesNeeded() > 0)
                    {
                        StartCoroutine(popText("No available torches"));
                    }
                    else
                    {
                        StartCoroutine(popText("INVENTORY FULL"));
                    }
                }
            }
        }
        else
        {
            actionUIText.text = "";
            infoUIText.text = "";
            actionIcon.SetActive(false);
        }
    }

    private void OrbCooldown()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            inCooldown = false;
            OrbTimer.SetActive(false);
            if (abilityEquipped == 1)
            {
                OrbUI.GetComponent<Image>().sprite = OrbUISprite;
            }
            else if (abilityEquipped == 2)
            {
                OrbUI.GetComponent<Image>().sprite = OrbUISelectedSprite;
            }
        }
        OrbTimerText.text = Mathf.Round(countdown).ToString();
    }

    private void AbilitySwap()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1))
        {
            abilityEquipped = 1;
            rayRange = 3;
            abilityEquipped = 1;
            CrosshairUI.SetActive(true);
            GreenZoneUI.SetActive(false);
            OrbReticleUI.SetActive(false);
            TorchUI.GetComponent<Image>().sprite = TorchUISelectedSprite;
            OrbUI.GetComponent<Image>().sprite = OrbUISprite;
            if (torches > 0)
            {
                playerTorch.SetActive(true);
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2))
        {
            abilityEquipped = 2;
            CrosshairUI.SetActive(false);
            TorchUI.GetComponent<Image>().sprite = TorchUISprite;
            OrbUI.GetComponent<Image>().sprite = OrbUISelectedSprite;
            OrbReticleUI.SetActive(true);
            OrbReticleUI.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
            playerTorch.SetActive(false);
        }
    }

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          Coroutines
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //makes popup text show for a short period of time
    private IEnumerator popText(string popString)
    {
        inPopCoroutine = true;
        popUpText.text = popString;

        popUpText.CrossFadeColor(Color.red, 0.5f, false, true);
        yield return new WaitForSeconds(1.2f);
        popUpText.CrossFadeColor(Color.clear, 0.5f, false, true);

        inPopCoroutine = false;
        yield return null;
    }
}
