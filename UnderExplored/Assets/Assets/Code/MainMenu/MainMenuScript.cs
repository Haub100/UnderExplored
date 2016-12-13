using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void loadLevelID(int levelNumberHere)
    {
        GameObject.Find("GameManager").GetComponent<GameManager>().setStartingLevel(levelNumberHere-1);
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
