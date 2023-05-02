using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePersist : MonoBehaviour
{
     void Awake()
    {
       int NumGameSessions = FindObjectsOfType<GameSession>().Length; // find all game session scripts/instances
       if(NumGameSessions > 1)
       {
           Destroy(gameObject);
       } 
       else
       {
           DontDestroyOnLoad(gameObject); // if less than 1 then don't destroy gamesession script
       }
    }

    public void ResetScenePersist()
    {
        Destroy(gameObject);
    }

}
