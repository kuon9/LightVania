using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] AudioClip chcekPointSFX;
    AudioSource audioSource;

    private bool isChecked;
    
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isChecked = false;    
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player") && (!isChecked))
        {
            GameSession.playerLives++;
            Debug.Log("New Checkpoint acquired");
            audioSource.PlayOneShot(chcekPointSFX);
            isChecked = true;
            FindObjectOfType<PlayerMovement>().Checkpoint = transform.position;
        }
    }
}
