using UnityEngine;
using System.Collections;

public class doorFrameSound : MonoBehaviour
{
    [SerializeField]
    private AudioClip doorOpenSound;
	[SerializeField]
	private AudioClip doorCloseSound;
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
            audioSource.clip = doorOpenSound;
			audioSource.Play();
        }
        else if (!this.GetComponent<NodeController>().getIsLitEnough() && soundPlayed)
        {
			audioSource.clip = doorCloseSound;
			audioSource.Play();
            soundPlayed = false;
        }
    }
}
