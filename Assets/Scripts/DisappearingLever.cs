using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DisappearingLever : MonoBehaviour
{
    [SerializeField] GameObject [] disappearingGameObjects;
    [SerializeField] GameObject popUpText;
    [SerializeField] AudioClip doorOpenSFX;
     AudioSource audioSource;
    
    public bool playerinRange;
    public bool IsUsed;
    //public bool IsVanishing;
    
    
    // Start is called before the first frame update
    void Start()
    {
        IsUsed = false;
        audioSource = GetComponent<AudioSource>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerinRange) {return ;}
        VanishTiles();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !IsUsed)
        {
            popUpText.SetActive(true);
            playerinRange = true;    
        }    
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            popUpText.SetActive(false);
            playerinRange = false;
            
        }
    }    

    void VanishTiles()
    {
        for(int i = 0; i < disappearingGameObjects.Length; i++)
        if(playerinRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            disappearingGameObjects[i].SetActive(false);
            audioSource.PlayOneShot(doorOpenSFX);
            IsUsed = true;   
        }
    }    
}
