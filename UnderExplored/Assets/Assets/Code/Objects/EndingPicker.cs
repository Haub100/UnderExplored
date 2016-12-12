using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class EndingPicker : MonoBehaviour
{

    public GameObject[] intros;
    public GameObject[] endings;

    private bool introActive;
    private int endingNum;

    // Use this for initialization
    void Start()
    {
        introActive = true;
        foreach (GameObject intro in intros)
        {
            intro.SetActive(false);
        }

        foreach (GameObject ending in endings)
        {
            ending.SetActive(false);
        }

        endingNum = Random.Range(0, 3);

        endings[endingNum].SetActive(true);
        intros[endingNum].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (introActive)
            {
				introActive = false;
                intros[endingNum].SetActive(false);
            }
            else
            {
				SceneManager.LoadScene(0);
            }
        }
    }
}
