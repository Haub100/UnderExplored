using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;                            // The amount of health the player starts the game with.
    public int currentHealth;
    private Color blackScreen = new Color(0f, 0f, 0f, 1f);                                   // The current health the player has.
    //public GameObject playerRoot;
    //public Slider healthSlider;                                 // Reference to the UI's health bar.
    //public Image damageImage;                                   // Reference to an image to flash on the screen on being hurt.
    //public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    //public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    //public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.


    Animator anim;                                              // Reference to the Animator component.
    //AudioSource playerAudio;                                    // Reference to the AudioSource component.
    //PlayerMovement playerMovement;                              // Reference to the player's movement.
    //PlayerShooting playerShooting;                              // Reference to the PlayerShooting script.
    bool isDead;                                                // Whether the player is dead.
    bool damaged;                                               // True when the player gets damaged.
    private Image fadeToBlack;
    private GameObject roomManager;

    void Awake()
    {
        // Setting up the references.
        //anim = playerRoot.GetComponent<Animator>();
        //playerAudio = GetComponent <AudioSource> ();
        //playerMovement = GetComponent <PlayerMovement> ();
        //playerShooting = GetComponentInChildren <PlayerShooting> ();
        roomManager = GameObject.Find("RoomManager");

        // Set the initial health of the player.
        currentHealth = startingHealth;
        fadeToBlack = GameObject.Find("BlackPanel").GetComponent<Image>();
        fadeToBlack.color = blackScreen;
        //fadeToBlack.CrossFadeColor(Color.clear, 0.5f, false, true);
    }

    void Start(){
        spawn();
    }

    void Update()
    {
        // If the player has just been damaged...
        if (damaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            //damageImage.color = flashColour;
        }
        // Otherwise...
        else
        {
            // ... transition the colour back to clear.
            //damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        // Reset the damaged flag.
        damaged = false;
    }


    public void TakeDamage(int amount)
    {
        // Set the damaged flag so the screen will flash.
        damaged = true;

        // Reduce the current health by the damage amount.
        currentHealth -= amount;

        // Set the health bar's value to the current health.
        //healthSlider.value = currentHealth;

        // Play the hurt sound effect.
        //playerAudio.Play ();

        // If the player has lost all it's health and the death flag hasn't been set yet...
        if (currentHealth <= 0 && !isDead)
        {
            // ... it should die.
            Death();
        }
    }


    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;

        // Turn off any remaining shooting effects.
        //playerShooting.DisableEffects ();

        // Tell the animator that the player is dead.
        //anim.SetBool("isDead", true);

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play ();

        // Turn off the movement and shooting scripts.
        this.GetComponent<RigidbodyFirstPersonController>().enabled = false;
        this.GetComponent<CapsuleCollider>().enabled = false;
        this.GetComponent<Rigidbody>().isKinematic = true;
        //playerShooting.enabled = false;
        fadeToBlack.color = Color.black;
        fadeToBlack.CrossFadeColor(Color.black, 3f, false, true);

        StartCoroutine(respawn());
    }

    private void spawn()
    {
        roomManager.GetComponent<Checkpoints>().loadInventory();
        roomManager.GetComponent<Checkpoints>().loadCheckpoint();
        fadeToBlack.CrossFadeColor(Color.clear, 0.5f, false, true);
    }

    private IEnumerator respawn()
    {
        yield return new WaitForSeconds(4f);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        roomManager.GetComponent<RoomManager>().handleCheckpoint();
        roomManager.GetComponent<Checkpoints>().loadInventory();
        roomManager.GetComponent<Checkpoints>().loadCheckpoint();
        
        fadeToBlack.CrossFadeColor(Color.clear, 0.5f, false, true);
        this.GetComponent<RigidbodyFirstPersonController>().enabled = true;
        this.GetComponent<CapsuleCollider>().enabled = true;
        this.GetComponent<Rigidbody>().isKinematic = false;
        currentHealth = startingHealth;
        isDead = false;
    }
}
