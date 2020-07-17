using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public float walkingSpeed, runSpeed, jumpForce;
	public bool walking, running, flyingUp, fallingDown, jumpWasPressedAndIsPossible, jumping, crouching, dead, attacking, usingSpecialAttack;
    private Rigidbody2D rigidBody2d;
    private Animator animator;
	private SpriteRenderer spriteRenderer;

    public float timeBetweenBeingHurtInSec = 3;
    public float hurtTimer;


    public int maxHp = 5;
    private int hp;
    public int Hp
    {
        get { return hp; }
    }


    private float horizontalMoveInput, direction;
    private float moveSpeedWhileInAir = 0f;

    private BoxCollider2D playerCollider;

    private bool movingLeftDisabled, movingRightDisabled;

    private int points;
    public int Points
    {
        get { return points; }
    }

    public int pointsForCoin = 2;
    public int pointsForDiamond = 10;

	void Start () {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		spriteRenderer=GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
        walking = false;
		running=false;
		flyingUp=false;
		fallingDown=false;
		jumpWasPressedAndIsPossible=false;
        jumping = false;
		crouching=false;
        points = 0;
        hp = maxHp;
        hurtTimer = 0;
	}


    public void Update()
    {
        if(dead == false)
        {
            UpdateTimers();
            CheckMovementInput();
            CheckJumpingInput();
            CheckAttackInput();
            CheckCrouchInput();
            CheckSpecialAttackInput();
        }
    }

    private void UpdateTimers()
    {
        if(hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;
        }
    }

    private void CheckJumpingInput()
    {
        if (Input.GetKeyDown(KeyCode.X) && !jumping)
        {
            if (Mathf.Abs(rigidBody2d.velocity.y) < 2) // this line doesn't allow jump when in air
            {
                SetJumpSpeedAndMakeJumpOnNextFixedUpdate();
            }

        }
    }

    private void SetJumpSpeedAndMakeJumpOnNextFixedUpdate()
    {
        if (running)
        {
            moveSpeedWhileInAir = runSpeed;
        }
        else
        {
            moveSpeedWhileInAir = walkingSpeed;
        }
        jumpWasPressedAndIsPossible = true;
    }

    private void CheckMovementInput()
    {
        if(!attacking && !crouching && !usingSpecialAttack)
        {
            horizontalMoveInput = Input.GetAxisRaw("Horizontal");
            if (horizontalMoveInput != 0)
            {
                if (Input.GetKey(KeyCode.Z))
                {
                    //Run
                    walking = false; running = true;
                }
                else
                {
                    // Walk
                    walking = true; running = false;
                }
            }
            // If no arrow is pressed to move
            else
            {
                StopMoving();
            }
        } 
    }

    private void StopMoving()
    {
        walking = false; running = false;
        // disable player's velocity immediately
        Vector3 noHorizontalMoveVel = new Vector2(0, rigidBody2d.velocity.y);
        rigidBody2d.velocity = noHorizontalMoveVel;
    }

    void FixedUpdate () {
        Movement();
        Jump();
        CheckIfShouldDoFlyingUpOrFallingDownAnimation();
    }

	void Movement()
    {
        CheckIfShouldDisableMovement();

        ApplyMovementForcesToPlayer();

        ChangeDirectionOfPlayerSprite();

        CheckIfShouldDoWalkingAnimation();

        CheckIfShouldDoRunningAnimation();
    }

    private void CheckIfShouldDoRunningAnimation()
    {
        if (rigidBody2d.velocity.x != 0 && running == true)
        {
            animator.SetBool("Running", true);
        }
        else
        {
            animator.SetBool("Running", false);
        }
    }

    private void CheckIfShouldDoWalkingAnimation()
    {
        if (rigidBody2d.velocity.x != 0 && walking == true)
        {
            animator.SetBool("Walking", true);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }

    private void ChangeDirectionOfPlayerSprite()
    {
        if (rigidBody2d.velocity.x < 0 && !attacking)
        {
            spriteRenderer.flipX = true;
            animator.SetBool("LookingLeft", true);
        }
        else if (rigidBody2d.velocity.x > 0 && !attacking)
        {
            spriteRenderer.flipX = false;
            animator.SetBool("LookingLeft", false);
        }
    }

    private void ApplyMovementForcesToPlayer()
    {
        if (jumping)
        {
            rigidBody2d.velocity = new Vector2(horizontalMoveInput * moveSpeedWhileInAir * Time.deltaTime, rigidBody2d.velocity.y);
        }
        else if (running)
        {
            //Run
            rigidBody2d.velocity = new Vector2(horizontalMoveInput * runSpeed * Time.deltaTime, rigidBody2d.velocity.y);
        }
        else if (walking)
        {
            //Walk
            rigidBody2d.velocity = new Vector2(horizontalMoveInput * walkingSpeed * Time.deltaTime, rigidBody2d.velocity.y);
        }
    }

    private void CheckIfShouldDisableMovement()
    {
        if (movingLeftDisabled && horizontalMoveInput < 0)
        {
            horizontalMoveInput = 0;
        }
        if (movingRightDisabled && horizontalMoveInput > 0)
        {
            horizontalMoveInput = 0;
        }
    }

    void Jump()
    {
        if (jumpWasPressedAndIsPossible)
        {
            ApplyJumpForce();
        }
    }

    private void ApplyJumpForce()
    {
        rigidBody2d.AddForce(new Vector2(0, jumpForce));
        jumpWasPressedAndIsPossible = false;
        jumping = true;
        crouching = false;
        rigidBody2d.gravityScale = 1;
    }

    private void CheckIfShouldDoFlyingUpOrFallingDownAnimation()
    {
        // Flying up animation
        if (rigidBody2d.velocity.y > 2)
        {
            DoFlyingUpAnimation();
        }
        // Falling animation
        else if ((flyingUp && rigidBody2d.velocity.y <= 0) || rigidBody2d.velocity.y < -2)
        {
            DoFallingDownAnimation();
        }
    }

    private void DoFallingDownAnimation()
    {
        flyingUp = false; fallingDown = true;
        animator.SetTrigger("Down");
    }

    private void DoFlyingUpAnimation()
    {
        flyingUp = true; fallingDown = false;
        animator.SetTrigger("Up");
    }

    void CheckAttackInput(){                                                              
        if (!jumping && !attacking && !usingSpecialAttack)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                PerformAttack();
            }
        }
		
	}

    private void PerformAttack()
    {
        Vector3 noHorizontalMoveVel = new Vector2(0, rigidBody2d.velocity.y);
        rigidBody2d.velocity = noHorizontalMoveVel;
        animator.SetTrigger("Attack");
        attacking = true;

        walking = false; running = false;
    }

    void AttackEnd()
    {
		attacking=false;
	}

	void CheckSpecialAttackInput(){
        if (Input.GetKey(KeyCode.Space) && !jumping && !attacking)
        {
            PerformSpecialAttack();
        }
        else
        {
            SpecialAttackEnd();
        }
    }

    private void SpecialAttackEnd()
    {
        usingSpecialAttack = false;
        animator.SetBool("Special", false);
    }

    private void PerformSpecialAttack()
    {
        walking = false; running = false; rigidBody2d.velocity = Vector2.zero;
        usingSpecialAttack = true;
        animator.SetBool("Special", true);
    }

    void CheckCrouchInput(){
        if(attacking == false && jumping == false)
        {
            if (Input.GetKey(KeyCode.DownArrow))
            {
                PerformCrouch();
            }
            else
            {
                CrouchEnd();
            }
        }
        
    }

    private void CrouchEnd()
    {
        crouching = false;
        animator.SetBool("Crouching", false);
    }

    private void PerformCrouch()
    {
        walking = false; running = false; rigidBody2d.velocity = Vector2.zero;
        crouching = true;
        animator.SetBool("Crouching", true);
    }

    void OnTriggerEnter2D(Collider2D other){							// Case of bullet hit
		if(other.tag=="Enemy"){
			Hurt();
		}
        else if (other.tag == "Coin")
        {
            points += pointsForCoin;
            Destroy(other.gameObject);
        }
        else if (other.tag == "Diamond")
        {
            points += pointsForDiamond;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.tag == "Life")
        {
            if (hp < maxHp)
            {
                hp += 1;
                Destroy(other.gameObject);
            }
        }
        else if (other.gameObject.tag == "BottomBoundary")
        {
            Invoke("DeathAfterFallOut", 3);
        }

    }
    
    void DeathAfterFallOut()
    {
        rigidBody2d.isKinematic = true;
        rigidBody2d.simulated = false;
        hp = 0;
    }

	void OnCollisionEnter2D(Collision2D other)
    {                       // Case of enemy touch
        if (other.gameObject.tag == "Enemy")
        {
            Hurt();
        }
        CheckIfShouldStopFallingDownOrFlyingUpAnimation(other);
    }

    private void CheckIfShouldStopFallingDownOrFlyingUpAnimation(Collision2D other)
    {
        if ((fallingDown) && other.gameObject.tag == "Ground")
        {
            fallingDown = false; flyingUp = false; jumping = false;
            animator.SetTrigger("Ground");
            rigidBody2d.gravityScale = 3;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        CheckIfPlayerIsPushingAgainstTheWall(collision);
        if (collision.gameObject.tag == "Enemy")
        {
            Hurt();
        }
    }

    private void CheckIfPlayerIsPushingAgainstTheWall(Collision2D collision)
    {
        if (!attacking && !usingSpecialAttack && !crouching && (walking || running || jumping))
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 playerColliderCenter = playerCollider.bounds.center;

            bool isCollisionFromPlayersRight = contactPoint.x > playerColliderCenter.x;
            bool isCollisionFromPlayersLeft = contactPoint.x < playerColliderCenter.x;

            CheckIfPlayerIsPushingWallOnHisLeft(isCollisionFromPlayersLeft);

            CheckIfPlayerIsPushingWallOnHisRight(isCollisionFromPlayersRight);
        }
    }

    private void CheckIfPlayerIsPushingWallOnHisRight(bool isCollisionFromPlayersRight)
    {
        if (isCollisionFromPlayersRight)
        {
            if (horizontalMoveInput > 0)
            {
                if (rigidBody2d.velocity.x < 0.01)
                {
                    StopPlayerFromPushingWallOnHisRight();
                }
            }
        }
    }

    private void StopPlayerFromPushingWallOnHisRight()
    {
        horizontalMoveInput = 0;
        movingRightDisabled = true; movingLeftDisabled = false;
    }

    private void CheckIfPlayerIsPushingWallOnHisLeft(bool isCollisionFromPlayersLeft)
    {
        if (isCollisionFromPlayersLeft)
        {
            // If user wants to go left - towards collision ...
            if (horizontalMoveInput < 0)
            {
                // If player is not moving because of pushing against collision ...
                if (rigidBody2d.velocity.x > -0.01)
                {
                    // ... stop him from pushing on that collision, because he now floats in the air
                    StopPlayerFromPushingWallOnHisLeft();
                }
            }
        }
    }

    private void StopPlayerFromPushingWallOnHisLeft()
    {
        horizontalMoveInput = 0;
        movingLeftDisabled = true; movingRightDisabled = false;
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        movingLeftDisabled = false; movingRightDisabled = false;
    }

    void Hurt(){
		if (hurtTimer <= 0)
        {
            hurtTimer = timeBetweenBeingHurtInSec;
            animator.SetTrigger("Damage");
            hp -= 1;
        }
	}

	void Dead(){
		if(hp <= 0){
			animator.SetTrigger("Dead");
			dead=true;
		}
	}

	public void TryAgain(){														//Just to Call the level again
		SceneManager.LoadScene(0);
	}
}
