using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject door;
 
    
    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            door.GetComponent<Animator>().Play("Opening");
            Debug.Log("Opening");
        }
    }


    // void OnTriggerEnter2D(Collider2D other) 
    // {
    //     if(other.tag == "Player")
    //     {
    //         door.GetComponent<Animator>().Play("Opening");    
    //     }    
    // }



    // void OnTriggerExit2D(Collider2D other)
    // {
    //     if(other.tag == "Player")
    //     {
    //         door.GetComponent<Animator>().Play("Closing");
    //     }    
    // } 


    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            door.GetComponent<Animator>().Play("Closing");
            Debug.Log("Closing");
        }
    }

}
