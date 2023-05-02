using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    [SerializeField] GameObject [] fireworks;

    void OnTriggerEnter2D(Collider2D other) 
    {
        for(int i = 0; i < fireworks.Length; i++)
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("CONGRATS WOOOO");
            fireworks[i].SetActive(true);
        }    
    }
}
