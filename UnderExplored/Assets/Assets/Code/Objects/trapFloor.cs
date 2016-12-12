using UnityEngine;
using System.Collections;

public class trapFloor : MonoBehaviour
{

    Animator crumbleAnimation;
    bool isCrumbled = false;
    private GameObject player;
    private GameObject trapCollider;
    private AudioSource audioSource;
    public AudioClip crumble;

    // Use this for initialization
    void Start()
    {
        crumbleAnimation = GetComponent<Animator>();
        player = GameObject.Find("Player");
        trapCollider = GameObject.Find("TrapFloorCollider");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerHealth>().getIsDead())
        {
            crumbleAnimation.SetBool("isCrumbled", false);
			isCrumbled = false;
        }
    }

    public void setIsCrumbled(bool crumbled)
    {
        isCrumbled = crumbled;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !isCrumbled)
        {
            //Debug.Log("Trig Enter");
            isCrumbled = true;
            crumbleAnimation.SetBool("isCrumbled", true);
            Vector3 downVector = trapCollider.transform.position;
            downVector.y = -16f;
            trapCollider.transform.position = downVector;
            // Play Sound
            audioSource.clip = crumble;
            audioSource.Play();
        }
    }
}
