using UnityEngine;
using System.Collections;

public class HelpOverlay : MonoBehaviour
{

    private GameObject greyPanel;
    private GameObject indicatorText;
    private GameObject abilityText;
    private GameObject torchHelpText;
	private GameObject pressKeyHelp;
	private bool helpActivated;

    // Use this for initialization
    void Start()
    {
        greyPanel = GameObject.Find("GreyPanel");
        indicatorText = GameObject.Find("IndicatorText");
        abilityText = GameObject.Find("AbilityText");
        torchHelpText = GameObject.Find("TorchHelpText");
		pressKeyHelp = GameObject.Find("PressKeyHelp");
		

        greyPanel.SetActive(false);
        indicatorText.SetActive(false);
        torchHelpText.SetActive(false);
        abilityText.SetActive(false);
		pressKeyHelp.SetActive(false);
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
            torchHelpText.SetActive(false);
            abilityText.SetActive(false);
			pressKeyHelp.SetActive(false);
			helpActivated = false;
        }
		else if(Input.GetKeyUp(KeyCode.H))
		{
			setPanelsActive();
		}
    }

    public void setPanelsActive()
    {
		helpActivated = true;
        greyPanel.SetActive(true);
        indicatorText.SetActive(true);
        torchHelpText.SetActive(true);
        abilityText.SetActive(true);
		pressKeyHelp.SetActive(true);
		Time.timeScale = 0.0f;
    }
}
