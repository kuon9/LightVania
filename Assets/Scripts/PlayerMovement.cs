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
    [SerializeField] float distanceToCheck = 0.5f;

    Vector2 MovementInput;

    public bool isAlive = true;
    public bool isGrounded;


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
        {
            Run();
            SpriteDirection();    
        }    
    }

    void OnMove(InputValue input)
    {
        if(!isAlive) {return;}
        MovementInput = input.Get<Vector2>();
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
        if(!isTouchingGround)
        {
           return;
        }
        if(input.isPressed)
        {
            rigidbody.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
            anim.SetBool("IsJumping", isTouchingGround);
        }  
    }

    void OnTriggerEnter2D()
    {
        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            anim.SetBool("IsJumping", false);
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
