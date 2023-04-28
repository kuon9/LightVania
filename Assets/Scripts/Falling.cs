using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling : MonoBehaviour
{

    [SerializeField] int timeToDisappear = 3;
    Rigidbody2D rb;
    PlayerMovement playermovement;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playermovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();    
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            rb.isKinematic = false;
            Destroy(this.gameObject, timeToDisappear);
        }
    }
    
    
    void OnCollisionEnter2D(Collision2D col)
    {
       if(col.gameObject.tag =="Player")
       {
            StartCoroutine(playermovement.Respawn()); 
       }
    } 
}

