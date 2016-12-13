using UnityEngine;
using System.Collections;

public class evilChoice_Fire : MonoBehaviour
{

    public GameObject fireWall;

    [SerializeField]
    private AudioClip pullLever;
    private AudioSource audioSource;

    // Use this for initialization
    void Start()
    {
        fireWall.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    public void activate()
    {
        fireWall.SetActive(true);
        this.gameObject.GetComponent<Animator>().SetTrigger("LeverPull");
        audioSource.clip = pullLever;
        audioSource.Play();
    }

    public void checkPointReset()
    {
        fireWall.SetActive(false);
    }

}
