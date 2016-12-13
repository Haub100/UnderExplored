﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
    private GameObject roomManager; //room manager that controls the level
    private GameObject gameManager;
    private LayerMask actionItems; //all objects that can be used with the action 'E' key
    private Ray actionRay; //Ray that comes out from the middle of the player camera
    private RaycastHit hit;
    private Vector3 torchSize = new Vector3(1f, 1f, 1f);
    private Vector3 instantiateLocation; //the location a spell is instantiated
    private Vector3 instantateDirection; //the direction a spell travels when cast
    private bool isCharging = false; //used for charging abilities
    private bool inCooldown = false;
    private float force; //used for casting spells a specific distance
    private float heightWidth; //determines the width and the height of the orb reticle
    private int torches;
    private float countdown; //countdown timer used for UI cooldowns
    private Animator torchAnimator;
    private List<bool> activatedAbilities = new List<bool>(); //Holds a list of which abilities are activated or not
    private AudioSource oneShotSFX; //Sound Effects that occur once and are done (torchlight, torchdouse)
    private AudioSource constantSounds; //Sounds that play consistently (walking)

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
    private Sprite TorchUIGreyscaleSprite;
    private string actionString;
    private bool inPopCoroutine = false;

    //Sound
    [SerializeField]
    private AudioClip torchPlace;
    [SerializeField]
    private AudioClip torchDouse; 
    private AudioSource audioSource; 

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
        torchAnimator = playerTorch.GetComponent<Animator>();
        roomManager = GameObject.Find("RoomManager");
        gameManager = GameObject.Find("GameManager");
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
        TorchUIGreyscaleSprite = (Sprite)Resources.Load("Textures/TorchUIGrey", typeof(Sprite));
        TorchUI.GetComponent<Image>().sprite = TorchUISelectedSprite;
        OrbUI.GetComponent<Image>().sprite = OrbUISprite;
        OrbReticleUI.SetActive(false);
        GreenZoneUI.SetActive(false);
        actionUIText.text = "";
        actionIcon = GameObject.Find("Interact Icon");
        actionIcon.SetActive(false);
        OrbTimer.SetActive(false);

        activatedAbilities.Add(true);
        activatedAbilities.Add(false);
        ActiveCheck();

        //Sound
        audioSource = GetComponent<AudioSource>();
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
            if (torches == 0)
            {
                playerTorch.SetActive(false);
            }
            else
            {
                playerTorch.SetActive(true);
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
                    if (this.GetComponent<Inventory>().getTorches() > 0)
                    {
                        this.GetComponent<Inventory>().removeTorches(1);
                        audioSource.clip = torchPlace;
                        audioSource.Play();
                        if(gameManager.GetComponent<GameManager>().getOpenHelpOverlay())
                        {
                            roomManager.GetComponent<HelpOverlay2>().setPanelsActive();
                            gameManager.GetComponent<GameManager>().setOpenHelpOverlay(false);
                        }
                    }

                    if (this.GetComponent<Inventory>().getTorches() == 0)
                    {
                        TorchUI.GetComponent<Image>().sprite = TorchUIGreyscaleSprite;
                        playerTorch.SetActive(false);
                    }
                    else
                    {
                        torchAnimator.SetTrigger("torchPlace");
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
                    TorchUI.GetComponent<Image>().sprite = TorchUISelectedSprite;
                    audioSource.clip = torchDouse;
                    audioSource.Play();

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

        if (Input.GetMouseButtonUp(0))
        {
            if (torchAnimator.isActiveAndEnabled)
            {
                torchAnimator.SetTrigger("mouseUp");
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

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          Action Items (e button)
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
                        if (abilityEquipped == 1)
                        {
                            TorchUI.GetComponent<Image>().sprite = TorchUISelectedSprite;
                            playerTorch.SetActive(true);
                        }
                        else
                        {
                            TorchUI.GetComponent<Image>().sprite = TorchUISprite;
                        }
                        hit.transform.gameObject.GetComponent<Animator>().SetTrigger("OpenChest");

                        if(gameManager.GetComponent<GameManager>().getOpenHelpOverlay())
                        {
                            roomManager.GetComponent<HelpOverlay>().setPanelsActive();
                            //gameManager.GetComponent<GameManager>().setOpenHelpOverlay(false);
                        }
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
            else if (hit.transform.gameObject.tag == "OrbUnlock")
            {
                if (!activatedAbilities[1])
                {
                    actionUIText.text = "Press 'E' to GAIN ABILITY: Light Orb";
                }
                else
                {
                    actionUIText.text = "";
                    actionIcon.SetActive(false);
                }

                if (Input.GetKeyDown(KeyCode.E))
                {
                    activatedAbilities[1] = true;
                    ActiveCheck();
                }
            }
            else if (hit.transform.gameObject.tag == "ThiefGhost")
            {

                actionUIText.text = "Press 'E' to give torches";


                if (Input.GetKeyDown(KeyCode.E))
                {
                    this.gameObject.GetComponent<Inventory>().setTorches(0);
                    TorchUI.GetComponent<Image>().sprite = TorchUIGreyscaleSprite;
                    playerTorch.SetActive(false);
                }
            }
            else if (hit.transform.gameObject.tag == "EvilLever")
            {

                actionUIText.text = "Press 'E' to be evil";


                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.transform.gameObject.GetComponent<evilChoice_Fire>().activate();
                    roomManager.GetComponent<RoomManager>().addPoints(-100);
                }
            }
            else if (hit.transform.gameObject.tag == "EndGame")
            {
                actionUIText.text = "Press 'E' to set status to: Explored";

                if (Input.GetKeyDown(KeyCode.E))
                {
                    roomManager.GetComponent<RoomManager>().endDungeon();
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

    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //          Abilities Active Check and Swapping
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    private void AbilitySwap()
    {
        if (Input.GetKeyUp(KeyCode.Alpha1) && activatedAbilities[0])
        {
            abilityEquipped = 1;
            rayRange = 3;
            abilityEquipped = 1;
            CrosshairUI.SetActive(true);
            GreenZoneUI.SetActive(false);
            OrbReticleUI.SetActive(false);
            OrbUI.GetComponent<Image>().sprite = OrbUISprite;
            if (torches > 0)
            {
                playerTorch.SetActive(true);
            }

            if (this.GetComponent<Inventory>().getTorches() == 0)
            {
                TorchUI.GetComponent<Image>().sprite = TorchUIGreyscaleSprite;
            }
            else
            {
                TorchUI.GetComponent<Image>().sprite = TorchUISelectedSprite;
            }
        }
        else if (Input.GetKeyUp(KeyCode.Alpha2) && activatedAbilities[1])
        {
            abilityEquipped = 2;
            CrosshairUI.SetActive(false);
            TorchUI.GetComponent<Image>().sprite = TorchUISprite;
            OrbUI.GetComponent<Image>().sprite = OrbUISelectedSprite;
            OrbReticleUI.SetActive(true);
            OrbReticleUI.GetComponent<RectTransform>().sizeDelta = new Vector2(100f, 100f);
            playerTorch.SetActive(false);

            if (this.GetComponent<Inventory>().getTorches() == 0)
            {
                TorchUI.GetComponent<Image>().sprite = TorchUIGreyscaleSprite;
            }
            else
            {
                TorchUI.GetComponent<Image>().sprite = TorchUISprite;
            }
        }
    }

    //Handles showing or removing UI for active/inactive abilities
    public void ActiveCheck()
    {
        for (int x = 0; x < activatedAbilities.Count; x++)
        {
            if (!activatedAbilities[x] && x == 1)
            {
                OrbUI.SetActive(false);
            }
            else
            {
                OrbUI.SetActive(true);
            }
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
