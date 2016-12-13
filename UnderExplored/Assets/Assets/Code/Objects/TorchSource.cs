using UnityEngine;
using System.Collections;

public class TorchSource : MonoBehaviour
{

    public int torchCount; //number of torches available in the source
    public bool isChest;

    [SerializeField]
    private AudioClip openChest;
    private AudioSource audioSource;
    [SerializeField]
    private GameObject unlitTorches;

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int getTorchCount()
    {
        return torchCount;
    }

    public void setTorchCount(int torchAmount)
    {
        torchCount = torchAmount;
    }

    public int takeTorches(int torchesToTake)
    {
        if (torchesToTake > 0 && isChest)
        {
            audioSource.clip = openChest;
            audioSource.Play();
            StartCoroutine(deleteChestTorches());
        }

        if (torchesToTake <= torchCount)
        {
            torchCount -= torchesToTake;
            return torchesToTake;
        }
        else
        {
            int returnAmount = torchCount;
            torchCount -= torchCount;
            return returnAmount;
        }
    }

    IEnumerator deleteChestTorches()
    {
        yield return new WaitForSeconds(1.3f);
        unlitTorches.SetActive(false);
    }
}
