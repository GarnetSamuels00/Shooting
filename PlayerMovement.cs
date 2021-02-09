using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;

    private bool facingRight = true;

    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    private int extraJumps;
    public int extraJumpsMax;
    private bool canDoubleJump;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;


    private Animator anim;


    // Start is called before the first frame update
    void Start()
    {

        extraJumps = extraJumpsMax;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);


        moveInput = Input.GetAxisRaw("Horizontal");
        Debug.Log(moveInput);
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        if (moveInput == 0)
        {
            anim.SetBool("isRunning", false);
        }
        else
        {
            anim.SetBool("isRunning", true);
        }

        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }

    }

    void Update()
    {
        /*
        if(isGrounded == true)
        {
            isJumping = false;
            extraJumps = extraJumpsMax;
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }*/

        /*if (isGrounded == true)
        {
            isJumping = false;
            canDoubleJump = false;
            extraJumps = extraJumpsMax;
        }*/

        if (isGrounded == true && Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            extraJumps = extraJumpsMax;
            canDoubleJump = false;
            rb.velocity = Vector2.up * jumpForce;

        }

        if (Input.GetKey(KeyCode.UpArrow) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }
        else if (Input.GetKey(KeyCode.UpArrow) && isJumping == false && extraJumps > 0)
        {
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow) && extraJumps == 0 && isGrounded == true)
        {
            rb.velocity = Vector2.up * jumpForce;
        }



        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            isJumping = false;
        }



    }

    void Flip()
    {

        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;

    }
}
