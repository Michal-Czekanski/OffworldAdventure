using UnityEngine.SceneManagement;
using UnityEngine;
using System.Collections;
using System;

public class PlayerController : MonoBehaviour {

    public float walkingSpeed, runSpeed, jumpForce;
	public bool walking, running, flyingUp, fallingDown, applyJumpForce, jumping, crouching, dead, attacking, usingSpecialAttack;

    /// <summary>
    /// Rigidbody component of player game object.
    /// </summary>
    private Rigidbody2D rigidBody2d;

    /// <summary>
    /// Animator component of player game object.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Sprite renderer of player game object.
    /// </summary>
	private SpriteRenderer spriteRenderer;

    /// <summary>
    /// In seconds.
    /// </summary>
    public float hurtTimeInterval = 2;

    /// <summary>
    /// Is used to apply damage in time intervals.
    /// </summary>
    private float hurtTimer;


    public int maxHp = 5;
    private int hp;
    public int Hp
    {
        get { return hp; }
    }


    private float horizontalMoveInput, direction;
    private float moveSpeedWhileInAir = 0f;

    private BoxCollider2D playerCollider;

    /// <summary>
    /// Player earns points by collecting certain items.
    /// </summary>
    private int points;
    public int Points
    {
        get { return points; }
    }

    /// <summary>
    /// Amount of points for collecting coin.
    /// </summary>
    public int pointsForCoin = 2;

    /// <summary>
    /// Amount of points for collecting diamond.
    /// </summary>
    public int pointsForDiamond = 10;

    /// <summary>
    /// If player is near next level entrance he can finish current level.
    /// </summary>
    private bool nearNextLvlEntrance = false;

	void Start () {
        rigidBody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
		spriteRenderer=GetComponent<SpriteRenderer>();
        playerCollider = GetComponent<BoxCollider2D>();
        walking = false;
		running=false;
		flyingUp=false;
		fallingDown=false;
		applyJumpForce=false;
        jumping = false;
		crouching=false;
        points = 0;
        hp = maxHp;
        hurtTimer = 0;
	}


    public void Update()
    {
        if (!PauseManagerController.gamePaused)
        {
            if (dead == false)
            {
                UpdateTimers();
                CheckMovementInput();
                CheckJumpingInput();
                CheckAttackInput();
                CheckCrouchInput();
                CheckSpecialAttackInput();
                CheckGoToNextLevelInput();
                CheckIfShouldBeDead();
            }
        }
    }

    private void CheckGoToNextLevelInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && nearNextLvlEntrance)
        {
            GoToNextLevel();
        }
    }

    private void GoToNextLevel()
    {
        int sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex < sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(currentSceneIndex + 1, LoadSceneMode.Single);
        }
        else
        {
            LastLevelCompletion();
        }
    }

    private void LastLevelCompletion()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Updates all timers by measuring how much time passed since last frame.
    /// </summary>
    private void UpdateTimers()
    {
        if(hurtTimer > 0)
        {
            hurtTimer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Checks if player can jump and if user wants to jump == TRUE: sets appropiate values to perform jump.
    /// </summary>
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

    /// <summary>
    /// <para>
    /// Sets how fast player will move in the air while jumping and <br/>
    /// makes player jump on next FixedUpdate() call.
    /// </para>
    /// If player was walking and jump was pressed he will have slower jump speed. <br></br>
    /// If player was running and jump was pressed he will have faster jump speed.
    /// </summary>
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
        applyJumpForce = true; // This makes player jump on next FixedUpdate() call.
    }

    /// <summary>
    /// Checks if player can move and then checks movement inputs.
    /// </summary>
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
                StopHorizontalMovement();
            }
        } 
    }

    /// <summary>
    /// Disables horizontal velocity.
    /// </summary>
    private void StopHorizontalMovement()
    {
        walking = false; running = false;
        // disable player's velocity immediately
        Vector3 noHorizontalMoveVel = new Vector2(0, rigidBody2d.velocity.y);
        rigidBody2d.velocity = noHorizontalMoveVel;
    }

    void FixedUpdate () {
        if(dead == false)
        {
            Movement();
            Jump();
            CheckIfShouldDoFlyingUpOrFallingDownAnimation();
        }
        
    }

    /// <summary>
    /// Makes player move horizontally by applying certain forces and animations.
    /// </summary>
	void Movement()
    {
        ApplyHorizontalMovementForcesToPlayer();

        ChangeDirectionOfPlayerSprite();

        CheckIfShouldDoWalkingAnimation();

        CheckIfShouldDoRunningAnimation();
    }

    /// <summary>
    /// Starts or stops running animation.
    /// </summary>
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

    /// <summary>
    /// Starts or stops walking animation.
    /// </summary>
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

    /// <summary>
    /// Changes players sprite looking direction based on movement direction.
    /// </summary>
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

    /// <summary>
    /// Applies horizontal movement forces to player.
    /// </summary>
    private void ApplyHorizontalMovementForcesToPlayer()
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

    /// <summary>
    /// Checks if jump force should be applied and applies it.
    /// </summary>
    void Jump()
    {
        if (applyJumpForce)
        {
            ApplyJumpForce();
        }
    }

    /// <summary>
    /// Makes player jump by applying jump force and sets appropiate gravity scale for jumping.
    /// </summary>
    private void ApplyJumpForce()
    {
        rigidBody2d.AddForce(new Vector2(0, jumpForce));
        applyJumpForce = false;
        jumping = true;
        crouching = false;
        rigidBody2d.gravityScale = 1;
    }

    /// <summary>
    /// Checks if flying up or falling down animation should be played and plays it.
    /// </summary>
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

    /// <summary>
    /// Plays falling down animation.
    /// </summary>
    private void DoFallingDownAnimation()
    {
        flyingUp = false; fallingDown = true;
        animator.SetTrigger("Down");
    }


    /// <summary>
    /// Plays flying up animation.
    /// </summary>
    private void DoFlyingUpAnimation()
    {
        flyingUp = true; fallingDown = false;
        animator.SetTrigger("Up");
    }

    /// <summary>
    /// <list type="bullet">
    /// <item> Checks if attacking is possible, then ... </item>
    /// <item> Checks if user wants to attack, then ... </item>
    /// <item> Performs attack</item>
    /// </list>
    /// </summary>
    void CheckAttackInput(){                                                              
        if (!jumping && !attacking && !usingSpecialAttack)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                PerformAttack();
            }
        }
		
	}

    /// <summary>
    /// Performs attack.
    /// </summary>
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

    /// <summary>
    /// Performs special attack if:
    /// <list type="bullet">
    /// <item>user wants to attack</item>
    /// <item>special attack is possible</item>
    /// </list>
    /// </summary>
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

    /// <summary>
    /// Stops special attack.
    /// </summary>
    private void SpecialAttackEnd()
    {
        usingSpecialAttack = false;
        animator.SetBool("Special", false);
    }

    /// <summary>
    /// Performs special attack.
    /// </summary>
    private void PerformSpecialAttack()
    {
        walking = false; running = false; rigidBody2d.velocity = Vector2.zero;
        usingSpecialAttack = true;
        animator.SetBool("Special", true);
    }

    /// <summary>
    /// Performs crouch if:
    /// <list type="bullet">
    /// <item>user wants to crouch</item>
    /// <item>crouch is possible</item>
    /// </list>
    /// Also checks if shouuld stop crouching.
    /// </summary>
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

    /// <summary>
    /// Stops crouching.
    /// </summary>
    private void CrouchEnd()
    {
        crouching = false;
        animator.SetBool("Crouching", false);
    }

    /// <summary>
    /// Performs crouch.
    /// </summary>
    private void PerformCrouch()
    {
        walking = false; running = false; rigidBody2d.velocity = Vector2.zero;
        crouching = true;
        animator.SetBool("Crouching", true);
    }

    void OnTriggerEnter2D(Collider2D other){
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
    
    /// <summary>
    /// Player has fallen out of the game.
    /// </summary>
    void DeathAfterFallOut()
    {
        rigidBody2d.isKinematic = true;
        rigidBody2d.simulated = false;
        hp = 0;
    }

	void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Hurt();
        }
        CheckIfNearNextLvlEntrance(other);
        CheckIfShouldStopFallingDownOrFlyingUpAnimation(other);
    }

    /// <summary>
    /// Checks if player is near next level entrance.
    /// </summary>
    /// <param name="other"></param>
    private void CheckIfNearNextLvlEntrance(Collision2D other)
    {
        if (other.gameObject.tag == "Finish")
        {
            nearNextLvlEntrance = true;
        }
    }

    private void CheckIfShouldStopFallingDownOrFlyingUpAnimation(Collision2D other)
    {
        if ((fallingDown) && (other.gameObject.tag == "Ground" || other.gameObject.tag == "Finish"))
        {
            fallingDown = false; flyingUp = false; jumping = false;
            animator.SetTrigger("Ground");
            rigidBody2d.gravityScale = 3;
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Hurt();
        }
    }


    public void OnCollisionExit2D(Collision2D collision)
    {
        CheckIfNoLongerNearNextLvlEntrance(collision);
    }

    /// <summary>
    /// Checks if player is no longer near next level entrance.
    /// </summary>
    /// <param name="collision"></param>
    private void CheckIfNoLongerNearNextLvlEntrance(Collision2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            nearNextLvlEntrance = false;
        }
    }

    /// <summary>
    /// Deals damage to player.
    /// </summary>
    void Hurt(){
		if (hurtTimer <= 0)
        {
            hurtTimer = hurtTimeInterval;
            animator.SetTrigger("Damage");
            hp -= 1;
        }
	}

    /// <summary>
    /// Checks if player has 0 hp.
    /// </summary>
	void CheckIfShouldBeDead(){
		if(hp <= 0){
			animator.SetTrigger("Dead");
			dead=true;
		}
	}
}
