using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour
{
    
    [SerializeField] BoxCollider2D Collider;
    
    Animator anim;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();    
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("IsDisappearing",true);
        }
    }

    void Gone()
    {
        Collider.enabled = false;
        Destroy(this.gameObject);
    }

}
