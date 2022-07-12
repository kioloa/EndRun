using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public float moveSpeed; //speed
    public float speedMultiplier;
    private float moveSpeedStore;

    public float speedIncreaseMilestone;
    private float speedIncreaseMilestoneStore;
    private float speedMilestoneCount;
    private float speedMilestoneCountStore;

    public float jumpForce;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool stoppedJumping;
    private bool canDoubleJump;

    public float slidingForce;
    public float slidingTime;
    private float slidingTimeCounter;

    public bool Sliding;

    private Rigidbody2D myRigidbody;

    public bool grounded;
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float groundCheckRadius;

    private Animator myAnimator;

    public GameManager theGameManager;

    public AudioSource jumpSound;
    public AudioSource deathSound;

    public GameObject thePlayer;

    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        myAnimator = GetComponent<Animator>();
        jumpTimeCounter = jumpTime;
        slidingTimeCounter = slidingTime;
        speedMilestoneCount = speedIncreaseMilestone;
        moveSpeedStore = moveSpeed;
        speedMilestoneCountStore = speedMilestoneCount;
        speedIncreaseMilestoneStore = speedIncreaseMilestone;
        stoppedJumping = true;
        Sliding = false;
        thePlayer.transform.localScale = new Vector2(1f, 1f);
    }

    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        if (transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;

            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;

            moveSpeed = moveSpeed * speedMultiplier;
        }

        myRigidbody.velocity = new Vector2(moveSpeed, myRigidbody.velocity.y);

        //Прыжок вниз
        if (Input.GetMouseButtonDown(1))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -0.25f * jumpForce);
            Sliding = true;
            jumpSound.Play();
            thePlayer.transform.localScale = new Vector2(0.8f, 0.8f);
        }

        if (Input.GetMouseButton(1) && !stoppedJumping)
        {

            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -0.25f * jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }

        }

        if (Input.GetMouseButtonUp(1))
        {
            jumpTimeCounter = 0;
            Sliding = false;
            thePlayer.transform.localScale = new Vector2(1f, 1f);
        }

        //Прыжок
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            if (grounded)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                stoppedJumping = false;
                jumpSound.Play();
            }

            if (!grounded && canDoubleJump)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                stoppedJumping = false;
                canDoubleJump = false;
                jumpSound.Play();
            }
        }

        if((Input.GetKey (KeyCode.Space) || Input.GetMouseButton(0)) && !stoppedJumping)
        {

            if (jumpTimeCounter > 0)
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
       
        }

        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        if (grounded)
        {
            jumpTimeCounter = jumpTime;
            canDoubleJump = true;
        }

        myAnimator.SetFloat("Speed", myRigidbody.velocity.x);
        myAnimator.SetBool("Grounded", grounded);
        myAnimator.SetBool("Sliding", Sliding);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "killbox")
        {
            theGameManager.RestartGame();
            moveSpeed = moveSpeedStore;
            speedMilestoneCount = speedMilestoneCountStore;
            speedIncreaseMilestone = speedIncreaseMilestoneStore;
            deathSound.Play();
        }
    }
}
