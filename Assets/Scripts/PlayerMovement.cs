using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public GameObject player;
    PlayerInput input;
     
    [Header("PlayerAttributes")]
    [SerializeField] float playerSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] float fallGravityMultiplier = 2f;
    [SerializeField] float gravityScale = 1f;
    [SerializeField] Vector2 deathKick = new Vector2(0f,-5f); 
    


    [Header("SFX")]
    [SerializeField] AudioClip gameOverSFX;
    [SerializeField] AudioClip fireWorksSFX;
    [SerializeField] AudioClip arrowSfx;


    [Header("Arrow")]
     [SerializeField] GameObject  arrow;
     [SerializeField] Transform arrowSpawn;





    public bool isAlive = true;
    //bool isGrounded = true;
    //bool canMove;
    bool isJumping;

    AudioSource audioSource;
    Vector2 MovementInput;
    Animator anim;
    Rigidbody2D rigidbody;
    BoxCollider2D feetCollider;
    CapsuleCollider2D playerCollider;
    public Vector2 Checkpoint;
    
    
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
        playerCollider = GetComponent<CapsuleCollider2D>();
        input = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!isAlive|| LevelExit.isCleared) {return;}
        // if(canMove)
        {
            Run();
            SpriteDirection();
            Death();
        }    
    }

    void OnMove(InputValue input)
    {
        if(!isAlive || PauseMenu.isPaused || LevelExit.isCleared) {return;}
        // if(canMove)
        {
            MovementInput = input.Get<Vector2>();    
        }
    }

    public void OnRange(InputValue input)
    {
        if(!isAlive|| PauseMenu.isPaused || LevelExit.isCleared) {return;}
        {
            playerSpeed = 0f;
            audioSource.PlayOneShot(arrowSfx);
            //canMove = false;
            //anim.SetTrigger("IsShooting");
            anim.SetBool("IsShooting", true);
            Debug.Log("Firing");
            StartCoroutine(FireReset());    
        }
    }


    // public void OnMelee(InputValue input)
    // {
    //     if(!isAlive) {return;}
    //     {
    //         playerSpeed = 0f;
    //         //canMove = false;
    //         anim.SetBool("IsAttacking", true);
    //         //anim.SetTrigger("IsAttacking")
    //         StartCoroutine(AttackReset());
    //     }
    // }

    IEnumerator FireReset()
    {
        yield return new WaitForSeconds(0.8f);
        anim.SetBool("IsShooting", false);
        //canMove = true;
        playerSpeed = 4f;
    }

    // IEnumerator AttackReset()
    // {
    //     yield return new WaitForSeconds(0.8f);
    //     anim.SetBool("IsAttacking", false);
    //     //canMove = true;
    //     playerSpeed = 4f;
    // }

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

    void OnDown(InputValue input)
    {
        //we're creating down button to affect fall gravity multiplier;
        if(isJumping)
        {
            rigidbody.gravityScale = gravityScale * fallGravityMultiplier;
        }
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
            anim.SetBool("IsJumping", true);
            isJumping = true;
        }  
    }

    public void Death()
    {
        if(playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy","Light","Hazards")))
        {
            StartCoroutine(Respawn());
            Debug.Log("death");
        }
    }

    public IEnumerator Respawn()
    {
        print("Hit me");
        audioSource.PlayOneShot(gameOverSFX);
        //disables movement after setactive, this stops movement all together and prevents previous movement
        input.actions.FindAction("Move").Disable();
        playerCollider.enabled = false;
        isAlive = false;
        anim.SetTrigger("Death");
        rigidbody.velocity = deathKick;
        yield return new WaitForSeconds(2);
        FindObjectOfType<GameSession>().ProcessPlayerDeath(); // calling function from another script and activate it.
        player.SetActive(false);
        playerCollider.enabled = true;
        player.transform.position = Checkpoint;
        input.actions.FindAction("Move").Enable();
        player.SetActive(true);
        isAlive = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground","Hazards")))
        {
            anim.SetBool("IsJumping",false);
            isJumping = false;
            rigidbody.gravityScale = gravityScale;
        }
        else if (other.tag == "FireWorks")
        {
            audioSource.PlayOneShot(fireWorksSFX);
        }
        // else if(other.tag == "Hazards")
        // {
        //     Debug.Log("Taking damage");
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
