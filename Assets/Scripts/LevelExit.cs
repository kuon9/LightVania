using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelloadDelay = 2f;
    [SerializeField] float LevelExitSlowMoFactor = 0.2f;
    //[SerializeField] GameObject gameClearUI;
    public static bool isCleared = false;
    // public string menuscene = "MainMenu";
    // public string nextLevel = "Level 2";
    // public int levelToUnlock = 2;
    

    
    //public Fader fader;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player");
        {
            StartCoroutine(SlowEffect());
            //gameClearUI.SetActive(true);
            isCleared = true;
        }
    }

    IEnumerator SlowEffect()
    {
        Time.timeScale = LevelExitSlowMoFactor; // time slows down for 0.2f seconds.
        yield return new WaitForSecondsRealtime(levelloadDelay);
        Time.timeScale = 0f; // slow down into 0 frames.
        LoadNextLevel();
    }

    public void LoadNextLevel() // function has to be public to pop up runtime for UI buttons
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        //FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
        
        Time.timeScale = 1f; // resume back to 1 timescale for next level.
        isCleared = false;
    } 
}
