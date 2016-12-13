using UnityEngine;
using System.Collections;

public class HelpOverlay2 : MonoBehaviour
{

    private GameObject greyPanel;
    private GameObject indicatorText;
    private GameObject progressionHelp;
    private GameObject torchHelpText02;
    private GameObject pressKeyHelp;
    private bool helpActivated;

    // Use this for initialization
    void Start()
    {
        greyPanel = GameObject.Find("GreyPanel");
        indicatorText = GameObject.Find("IndicatorText");
        torchHelpText02 = GameObject.Find("TorchHelpText02");
        progressionHelp = GameObject.Find("ProgressionHelp");
        pressKeyHelp = GameObject.Find("PressKeyHelp");

        //torchHelpText02.SetActive(false);
        //progressionHelp.SetActive(false);
		helpActivated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return) || (Input.GetKeyUp(KeyCode.H) && helpActivated))
        {
            Time.timeScale = 1.0f;
            greyPanel.SetActive(false);
            indicatorText.SetActive(false);
            torchHelpText02.SetActive(false);
            progressionHelp.SetActive(false);
			pressKeyHelp.SetActive(false);
            helpActivated = false;
        }
        else if (Input.GetKeyUp(KeyCode.H))
        {
            setPanelsActive();
        }
    }

    public void setPanelsActive()
    {
        helpActivated = true;
        greyPanel.SetActive(true);
        indicatorText.SetActive(true);
        torchHelpText02.SetActive(true);
        progressionHelp.SetActive(true);
		pressKeyHelp.SetActive(true);
        Time.timeScale = 0.0f;
    }
}
