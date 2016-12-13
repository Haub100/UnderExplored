using UnityEngine;
using System.Collections;

public class bridgeAssembleSound : MonoBehaviour
{

    [SerializeField]
    private AudioClip bridgeAssembleSoundClip;
    private AudioSource audioSource;
    private bool soundPlayed;

    // Use this for initialization
    void Start()
    {
        soundPlayed = false;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<NodeController>().getIsLitEnough() && !soundPlayed)
        {
            soundPlayed = true;
            //audioSource.clip = bridgeAssembleSoundClip;
            //audioSource.Play();
			StartCoroutine(playSoundHelper());
        }
        else if (!this.GetComponent<NodeController>().getIsLitEnough() && soundPlayed)
        {
            soundPlayed = false;
        }
    }

    IEnumerator playSoundHelper()
    {
        yield return new WaitForSeconds(1.5f);

        if (soundPlayed)
        {
            audioSource.clip = bridgeAssembleSoundClip;
            audioSource.Play();
        }
    }
}
