using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    private int startingLevel;
    private bool openHelpOverlay;

    void Awake()
    {
        MakeThisTheOnlyGameManager();
        DontDestroyOnLoad(transform.gameObject);
        startingLevel = 0;
        openHelpOverlay = true;
    }

    void MakeThisTheOnlyGameManager()
    {
        if (GM == null)
        {
            DontDestroyOnLoad(this.gameObject);

            GM = this;
        }
        else
        {
			if(GM!= this)
			{
				Destroy (this.gameObject);
			}
        }
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setStartingLevel(int levelNumber)
    {
        startingLevel = levelNumber;
    }

    public int getStartingLevel()
    {
        return startingLevel;
    }

    public void setOpenHelpOverlay(bool needHelp)
    {
        openHelpOverlay = needHelp;
    }

    public bool getOpenHelpOverlay()
    {
        return openHelpOverlay;
    }
}
