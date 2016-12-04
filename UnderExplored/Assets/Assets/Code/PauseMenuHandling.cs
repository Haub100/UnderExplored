using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseMenuHandling : MonoBehaviour {

    public Texture cursorTexture;
    public GameObject pauseMenu;
    private PlayerHealth playerHealth;
    
    private bool isShowing;

	// Use this for initialization
	void Start () {
        isShowing = false;
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        //Cursor.SetCursor(cursorTexture);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = false;
        
        
    }
	
	// Update is called once per frame
	void Update () {
        
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
            checkForMenu();
    }

    public void checkForMenu()
    {
        isShowing = !isShowing;
        pauseMenu.SetActive(isShowing);
        if (isShowing)
        {
            Cursor.visible = true;
            Time.timeScale = 0.0f;
            
            
        }
            
        if (!isShowing)
        {
            Cursor.visible = false;
            Time.timeScale = 1.0f;
            
        }
            
        

    }

    public void resume()
    {
        checkForMenu();
    }

    public void respawn()
    {
        checkForMenu();
        playerHealth.TakeDamage(100);
    }

    public void returnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
