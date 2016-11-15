using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadLevelID(int levelNumberHere)
    {
        SceneManager.LoadScene(levelNumberHere);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
