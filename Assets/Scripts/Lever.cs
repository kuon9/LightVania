using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Lever : MonoBehaviour
{
    
    
    [SerializeField] GameObject door;
    [SerializeField] GameObject popUpText;
    [SerializeField] GameObject FirstLight;
    [SerializeField] GameObject SecondLight;
    door Door;


    public bool playerinRange;
    public bool IsUsed;

    public bool IsClosing;
    public bool IsOpening;

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
        CloseDoor();    
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
        if(playerinRange && Keyboard.current.eKey.wasPressedThisFrame && IsOpening )
        {
            door.GetComponent<Animator>().Play("Opening2");
            
            /* AudioSource sound */
            Platform.IsActive = true;
            popUpText.SetActive(false);
            IsUsed = true;
            FirstLight.SetActive(false);
            SecondLight.SetActive(true);
        }    
    }

    void CloseDoor()
    {
                                    // This is the equivalent of Input.GetKey(KeyCode.E)
        if(playerinRange && Keyboard.current.eKey.wasPressedThisFrame && IsClosing )
        {
            door.GetComponent<Animator>().Play("Closing2");
            
            /* AudioSource sound */
            popUpText.SetActive(false);
            IsUsed = true;
            FirstLight.SetActive(false);
            SecondLight.SetActive(true);
        }        
    }
}
