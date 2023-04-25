using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameSession : MonoBehaviour
{
    public static int playerLives = 3;
    //[SerializeField] int score = 0;
    [SerializeField] TextMeshProUGUI livesText;
    //[SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject player;
    //[SerializeField] Fader fader;
    

    // void Awake()
    // {
    //    int NumGameSessions = FindObjectsOfType<GameSession>().Length; // find all game session scripts/instances
    //    if(NumGameSessions > 1)
    //    {
    //        Destroy(gameObject);
    //    } 
    //    else
    //    {
    //        DontDestroyOnLoad(gameObject); // if less than 1 then don't destroy gamesession script
    //    }
    // }

    void Start()
    {
        livesText.text = playerLives.ToString();
        //scoreText.text = score.ToString();
    }

    void Update()
    {
        livesText.text = playerLives.ToString();// updates the live count in UI    
    }

    public void ProcessPlayerDeath()
    {
        // If playerlives is greater than or equal to 1 
        if(playerLives >= 1) 
        {
            TakeLive();
        }
        else // if playerlives is less than 1 then do this method below
        {
            Destroy(player);
            Time.timeScale = 0f;
            //gameOverUI.SetActive(true);    
        }
    }
    
    // public void ResetGameSession()
    // {
    //     FindObjectOfType<ScenePersist>().ResetScenePersist();
    //     SceneManager.LoadScene(0); // go back to first scene which is probably the menu
    //     Destroy(gameObject);    
    // }

    public void TakeLive()
    {
        
        playerLives--;
        // int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        // SceneManager.LoadScene(CurrentSceneIndex);
        livesText.text = playerLives.ToString();
    }

    // public void AddScore(int addPoints)
    // {
    //     score += addPoints;
    //     scoreText.text = score.ToString();
    // }
    
    public void AddLife()
    {
        playerLives++;
    }

}

