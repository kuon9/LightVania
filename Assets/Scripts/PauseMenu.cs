using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject PauseUI;
    //allowed to be accessed by other classes.
    public static bool isPaused;
    private InputAction menu;
    // name of Action map is TileVania
    private LightVania  lightVania;
    //public Fader fader;
    

    void Awake()
    {
        lightVania = new LightVania();
    }

    void Start()
    {
    }

    private void OnEnable() 
    {
        // Getting escape key binding in TileVania action map and under Menu.
        menu = lightVania.Menu.Escape;
        menu.Enable();    

        // Whenever this action performs, it causes Pause function to fire also.
        menu.performed += Pause;
    }

    private void OnDisable() 
    {
        menu.Disable();
    }


    void Pause(InputAction.CallbackContext context)
    {
        // inverse
        isPaused = !isPaused;

        if(isPaused)
        {
            ActivateMenu();
            
        }    
        else
        {
            DeactivateMenu();
        }
    }

    public void ActivateMenu()
    {
        // Puts whole game in pause
        Time.timeScale = 0;
        AudioListener.pause = true;
        PauseUI.SetActive(true);    
    }

    public void DeactivateMenu()
    {
        // resumes game from pause
        Time.timeScale = 1;
        AudioListener.pause = false;
        PauseUI.SetActive(false);
        isPaused = false;  
    }

    // This function has to be public to be accessed in runtime for UI buttons.
    public void Restart()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene(currentSceneIndex);
        // Game is paused when restarting so this timescale solves it.
        Time.timeScale = 1;
        AudioListener.pause = false;
        GameSession.playerLives = 3;
        isPaused = false;
    }
    
    public void LoadMainMenu()
	{
		SceneManager.LoadScene (0); // scene 0 is the menu.
        // Game is paused after going back to main menu so this timescale solves it.
        Time.timeScale = 1; 
        LevelExit.isCleared = false;
        GameSession.playerLives = 3;
        isPaused = false;
	}

}
