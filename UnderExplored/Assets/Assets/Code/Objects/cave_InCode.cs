using UnityEngine;
using System.Collections;

public class cave_InCode : MonoBehaviour
{

    Animator caveInAnimation;
    bool isCollapsed = false;
    private GameObject player;
    [SerializeField]
    private AudioClip caveInSound;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        caveInAnimation = GetComponent<Animator>();
        player = GameObject.Find("Player");
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerHealth>().getIsDead())
        {
            caveInAnimation.SetBool("isCollapsible", false);
            isCollapsed = false;
        }
    }

    public void setIsCrumbled(bool crumbled)
    {
        isCollapsed = crumbled;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player") && !isCollapsed)
        {
            audioSource.clip = caveInSound;
            audioSource.Play();
            isCollapsed = true;
            caveInAnimation.SetBool("isCollapsible", true);
        }
    }
}
