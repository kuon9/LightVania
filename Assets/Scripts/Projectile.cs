using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
   
   
   [SerializeField] float arrowSpeed =  2f;
   [SerializeField] GameObject arrow;
   Rigidbody2D myRigidbody;
   BoxCollider2D boxCollider;
   PlayerMovement player; // referencing from our own PlayerMovement script
   float arrowVelocity;
 
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>(); // 
        player = FindObjectOfType<PlayerMovement>();
        boxCollider = GetComponent<BoxCollider2D>();
        // arrow represents X value in our new Vector2 on line 27
        arrowVelocity = player.transform.localScale.x * arrowSpeed; // Direction of player through scale of 1/-1 and instantiation arrow with arrowspeed float
        // Putting ArrowDirection under Start allows it to be permanent rule the moment I press play.
        ArrowDirection();
    }

    // Update is called once per frame
    void Update()
    {
        // putting -arrowVelocity makes arrow shoot backwards.
        myRigidbody.velocity = new Vector2(arrowVelocity, 0f); // (x,y) I want arrow to move 1 velocity horizontal
        //I tried putting this under update method and this made arrows change direction in midair as I changed player's directions.
        //ArrowDirection();
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Enemy")
        {
            // destroy gameobjects with tag "Enemy" only and not anything else
            Destroy(other.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        
        if(boxCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            Destroy(other.gameObject);
        }
        //  if i put (other.gameObject) then this arrow will destroy the gameobject with colliders enabled.
        if(other.gameObject.tag == "Ground")
        {
            Destroy(other.gameObject);    
        }
        else if (other.gameObject.tag == "Platform")
        {
            Destroy(other.gameObject);
        }
    }
    // Dictating arrow direction based on player's localscale.x, this allows arrow to face the correct direction.
    void ArrowDirection() 
    {
        if(player.transform.localScale.x == 1)
        {
            arrow.transform.rotation = Quaternion.Euler(0,0,0);
        }
        else if (player.transform.localScale.x == -1)
        {
            arrow.transform.rotation = Quaternion.Euler(0,0,180);
        }
    }

}
