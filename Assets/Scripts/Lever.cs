using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    
    GameJam gamejamControls;
    [SerializeField] GameObject door;
    [SerializeField] GameObject popUpText;
    [SerializeField] GameObject FirstLight;
    [SerializeField] GameObject SecondLight;


    public bool playerinRange;
    public bool IsUsed;

    // Start is called before the first frame update
    void Start()
    {
        IsUsed = false;    
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerinRange) {return ;}
        OpenDoor();    
    }



    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !IsUsed)
        {
            Debug.Log("In Range");
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


    void OpenDoor()
    {
                            // This is the equivalent of Input.GetKey(KeyCode.E)
        if(playerinRange && Keyboard.current.eKey.wasPressedThisFrame)
        {
            door.GetComponent<Animator>().Play("Opening2");
            /* AudioSource sound */
            popUpText.SetActive(false);
            IsUsed = true;
            FirstLight.SetActive(false);
            SecondLight.SetActive(true);
        }    
    }

}
