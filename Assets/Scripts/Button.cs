using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject door;
    [SerializeField] GameObject firstLight, secondLight;
 
    public bool IsOpening;
    // public bool IsClosing;



    void Update()
    {
    }
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player") && IsOpening)
        {
            door.GetComponent<Animator>().Play("Opening");
            Debug.Log("Opening");
            SwitchLights();
        }
        else //if(col.gameObject.CompareTag("Player") && IsClosing)
        {
            door.GetComponent<Animator>().Play("Closing");  
            SwitchLights();  
        }
    }


    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Projectile" && IsOpening)
        {
            door.GetComponent<Animator>().Play("Opening");
            SwitchLights();    
        }    
        else
        {
            door.GetComponent<Animator>().Play("Closing");
            SwitchLights();
            //IsOpening = false;
        }
    }

    void SwitchLights()
    {
        firstLight.SetActive(false);
        secondLight.SetActive(true);
    }



    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.tag == "Player")
    //     {
    //         door.GetComponent<Animator>().Play("Closing");
    //     }    
    // } 


    // void OnCollisionExit2D(Collision2D col)
    // {
    //     if(col.gameObject.CompareTag("Player"))
    //     {
    //         door.GetComponent<Animator>().Play("Closing");
    //         Debug.Log("Closing");
    //     }
    // }

}
