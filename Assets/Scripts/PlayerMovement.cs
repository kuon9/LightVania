using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    
    [Header("PlayerAttributes")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    

    [Header("Arrow")]
     [SerializeField] GameObject  arrow;
     [SerializeField] Transform arrowSpawn;

    Vector2 MovementInput;

    public bool isAlive = true;
    //bool canMove;

    AudioSource audioSource;
    Animator anim;
    Rigidbody2D rigidbody;
    BoxCollider2D feetCollider;
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();

            
    }

    // Update is called once per frame
    void Update()
    {
        if(!isAlive) {return;}
        // if(canMove)
        {
            Run();
            SpriteDirection();    
        }    
    }

    void OnMove(InputValue input)
    {
        if(!isAlive) {return;}
        // if(canMove)
        {
            MovementInput = input.Get<Vector2>();    
        }
    }

    public void OnRange(InputValue input)
    {
        if(!isAlive) {return;}
        {
            playerSpeed = 0f;
            //canMove = false;
            //anim.SetTrigger("IsShooting");
            anim.SetBool("IsShooting", true);
            Debug.Log("Firing");
            StartCoroutine(FireReset());    
        }
    }


    public void OnMelee(InputValue input)
    {
        if(!isAlive) {return;}
        {
            playerSpeed = 0f;
            //canMove = false;
            anim.SetBool("IsAttacking", true);
            //anim.SetTrigger("IsAttacking")
            StartCoroutine(AttackReset());
        }
    }

    IEnumerator FireReset()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("IsShooting", false);
        //canMove = true;
        playerSpeed = 4f;
    }

    IEnumerator AttackReset()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("IsAttacking", false);
        //canMove = true;
        playerSpeed = 4f;
    }

    void ProjectileFire()
    {
        Instantiate(arrow, arrowSpawn.position, Quaternion.identity);
    }


    void Run()
    {
        Vector2 playervelocity = new Vector2(MovementInput.x * playerSpeed, rigidbody.velocity.y);
        rigidbody.velocity = playervelocity;

        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        anim.SetBool("IsRunning", playerHasHorizontalSpeed);
    }

    
    void OnJump(InputValue input)
    {
        if(!isAlive) {return;}
        bool isTouchingGround = feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"));
        //if(!feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))  {return;}
        if(!isTouchingGround) {return;}
        if(input.isPressed)
        {
            rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", isTouchingGround);
        }  
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            anim.SetBool("IsJumping", false);
        }
        else if(other.tag == "Platform")
        {
            player.transform.parent = other.gameObject.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Platform")
        {
            player.transform.parent = null;
        }
    }

    void SpriteDirection()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(rigidbody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rigidbody.velocity.x), 1f);
        }
    }
}
