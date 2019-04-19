using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float speed = 8f;     //	Player speed
    public float speedMultiplier = 1f;
    public float crouchSpeedDivisor = 3f;   //  Reduce Speed when Crouching
    public float coyoteDuration = 0.05f;    //  How long player can jump after falling
    public float maxFallSpeed = -25f;   //  Max Speed player can fall

    [Header("Jumping")]
    public float jumpForce = 6.3f; //  Initial force of Jump
    private float jumpTime; //  Variable to hold jump duration
    private float coyoteTime;   //  Variable to hold coyote duration
    public float crouchJumpBoost = 2.5f;   // Give extra boost for jumping if player is crouching
    public float hangingJumpForce = 15f;  //  Force of wall hanging jump
    public float jumpHoldForce = 2f; //  Incremental force when jump is held
    public float jumpHoldDuration = 0.1f;  //  How long the jump key can be held

    [Header("Environment Check")]
    public bool drawDebugRaycasts = true;	//	Should the environment checks be visualized
    public float footOffset = 0.4f; //	X Offset of feet raycast
    public float eyeHeight = 1.5f;  //	Height of wall checks
    public float reachOffset = 0.7f;    //	X Offset for wall grabbing
    public float headClearance = 0.5f;  //	Space needed above the player's head
    public float groundDistance = 0.2f; //	Distance player is considered to be on the ground
    public float grabDistance = 0.4f;   // Wall grabbing distance
    public LayerMask groundLayer;   //	Layer of the ground

    [Header("Status flags")]
    public bool isOnGround;	//	Is player on Ground? 
    public bool isJumping;  //  Is player Jumping?
    public bool isCrouching;    //  Is player Crouching?
    public bool isHeadBlocked;  //  Is player's Head Blocked?
    public bool isHanging;  //  Is player Hanging on wall?

    //	Private Variables
    private Rigidbody2D rbody;              //	Rigidbody Component
    private BoxCollider2D bodyCollider;     //	Collider Component
    private InputManager inputManager;      //	Current Inputs for the player


    private float playerHeight;     //  Player Height
    private float originalXScale;   //  Original scale on X axis
    private int direction = 1;      //  Direction player is facing

    //  Standing Colliders
    private Vector2 colliderStandSize;  //  Size of the Standing Collider
    private Vector2 colliderStandOffset;    //  Offset of Standing Collider

    //  Crouching Colliders
    private Vector2 colliderCrouchSize; //  Size of the Crouching Collider
    private Vector2 colliderCrouchOffset;   //  Offset of Crouching Collider
    const float smallAmount = 0.5f;     //  Used for Hanging position

    private GameManager _gameManager;
    void Start()
    {
        //	Get a reference to the required components
        inputManager = GetComponent<InputManager>();
        rbody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

        //  Record Original x scale
        originalXScale = transform.localScale.x;

        //  Save the player height from the collider
        playerHeight = bodyCollider.size.y;

        //  Record the initial collider size and offset
        colliderStandSize = bodyCollider.size;
        colliderStandOffset = bodyCollider.offset;

        //  Calculate Crouching Collider size and offset
        colliderCrouchSize = new Vector2(bodyCollider.size.x, bodyCollider.size.y / 2f);
        colliderCrouchOffset = new Vector2(bodyCollider.offset.x, bodyCollider.offset.y / 2f);

        //  Setting player position to last checkpoint
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        transform.position = _gameManager.lastCheckPointPos;

    }

    void FixedUpdate()
    {
        //	Check the environment to determine status
        PhysicsCheck();

        //	Process ground
        GroundMovement();

        // Process Air Movements (Jump)
        MidAirMovement();
    }

    void PhysicsCheck()
    {
        //	Assume the player isn't on ground and the head isn't blocked
        isOnGround = false;
        isHeadBlocked = false;

        //	Cast rays for the left and right foot
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, groundDistance);
        RaycastHit2D rightCheck = Raycast(new Vector2(footOffset, 0f), Vector2.down, groundDistance);

        //  If either ray hits the ground, the player is on the ground
        if (leftCheck || rightCheck)
        {
            isOnGround = true;
        }

        //  Cast the ray to check above the player's head
        RaycastHit2D headCheck = Raycast(new Vector2(0f, bodyCollider.size.y), Vector2.up, headClearance);

        //  If that ray hits, the player's head is blocked
        if (headCheck)
        {
            isHeadBlocked = true;
        }

        //  Determine the direction of the wall grab attempt
        Vector2 grabDir = new Vector2(direction, 0f);

        //  Cast rays to look for a wall grab
        RaycastHit2D ledgeCheck = Raycast(new Vector2(reachOffset * direction, playerHeight), Vector2.down, grabDistance);
        RaycastHit2D wallCheck = Raycast(new Vector2(footOffset * direction, eyeHeight), grabDir, grabDistance);
        RaycastHit2D blockedCheck = Raycast(new Vector2(footOffset * direction, playerHeight), grabDir, grabDistance);

        //  If the player is off the ground
        //  AND is not Hanging
        //  AND is Falling
        //  AND found a ledge
        //  AND found a Wall

        if (!isOnGround && !isHanging && rbody.velocity.y < 0f
        && ledgeCheck && wallCheck && !blockedCheck)
        {
            //..We have ledge grab, Record the current position
            Vector3 pos = transform.position;

            //..move the player distance to the wall
            pos.x += (wallCheck.distance - smallAmount) * direction;

            //..move the player down to grab onto the ledge
            pos.y -= ledgeCheck.distance;

            //..apply this new position to the player
            transform.position = pos;

            //..set the rigidbody to static, so that player doen't fall down
            rbody.bodyType = RigidbodyType2D.Static;

            //..Set player is Hanging to true
            isHanging = true;
        }
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        //	Call the overloaded Raycast() method using the ground layermask
        //	and return the results
        return Raycast(offset, rayDirection, length, groundLayer);
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        //	Record the player's position
        Vector2 pos = transform.position;

        //	Send out the desired Raycast and record the result
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        //	Show Debug Raycasts in the Scene
        if (drawDebugRaycasts)
        {
            //	determine the color based on if the raycast hit
            Color color = hit ? Color.red : Color.green;

            //	draw ray in the scene
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }
        //	Return the results of the raycast
        return hit;
    }
    void GroundMovement()
    {
        //  If the player is currently hanging then can't move
        if (isHanging)
        {
            return;
        }

        //  Crouching Input
        //  ...If holding the crouch button
        //  ...AND is not currently crouching
        //  ...AND is on Ground
        if (inputManager.crouchHeld
            && !isCrouching
            && isOnGround)
        {
            Crouch();
        }
        //  If not Holding Crouching
        //  But is currenlty crouching
        //  Then Stand up
        else if (!inputManager.crouchHeld
                && isCrouching)
        {
            StandUp();
        }
        //  If Crouching
        //  But not on Ground
        //  Then Stand Up
        else if (!isOnGround
                && isCrouching)
        {
            StandUp();
        }


        //  Calculate the desired velocity based on inputs
        float xVelocity = speed * inputManager.horizontal;

        //  If the sign of the velocity and direction doesn't match,
        //  then flip the player
        if (xVelocity * direction < 0f)
        {
            FlipCharacterDirection();
        }

        //  If the player is Crouching
        //  then reduce the velocity
        if (isCrouching)
        {
            xVelocity /= crouchSpeedDivisor;
        }

        //  Apply the desired velocity
        rbody.velocity = new Vector2(xVelocity, rbody.velocity.y);

        //  If player is on Ground
        //  then extend the coyote time duration
        if (isOnGround)
        {
            coyoteTime = Time.time + coyoteDuration;
        }
    }

    void MidAirMovement()
    {
        //  If player is currently hanging,
        //  ...then cannot crouch
        //  ...but can jump

        if (isHanging)
        {
            //  If Crouch is pressed, Slide down
            if (inputManager.crouchPressed)
            {
                //  let go the hanging and slide down the wall
                isHanging = false;

                //  Set rigidbody to dynamic and exit
                rbody.bodyType = RigidbodyType2D.Dynamic;
                return;
            }

            //  If Jump is pressed
            if (inputManager.jumpPressed)
            {
                //  let go the hanging and slide down the wall
                isHanging = false;

                // Set rigidbody to dynamic, apply jump force and exit
                rbody.bodyType = RigidbodyType2D.Dynamic;
                rbody.AddForce(new Vector2(0f, hangingJumpForce), ForceMode2D.Impulse);
                return;
            }
        }

        //  If the JUMP key is pressed
        //  ...AND player isn't already jumping
        //  ...AND (either the player is on the Ground
        //  ...OR within the coyote time window) 

        if (inputManager.jumpPressed
            && !isJumping
            && (isOnGround || coyoteTime > Time.time))
        {
            //  if Crouching AND Head not blocked
            //  then Stand up and apply crouching jump boost
            if (isCrouching && !isHeadBlocked)
            {
                StandUp();
                rbody.AddForce(new Vector2(0f, crouchJumpBoost), ForceMode2D.Impulse);
            }

            //  Player is not on Ground And is jumping
            isOnGround = false;
            isJumping = true;

            //  Record the time the player will stop being able to boost their jump
            jumpTime = Time.time + jumpHoldDuration;

            //  Add the jump force
            rbody.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }

        //  Check if player is currently within the Jump time window,
        else if (isJumping)
        {
            //  ..and jump button is held
            //  ..then can apply JumpHoldforce
            if (inputManager.jumpHeld)
            {
                rbody.AddForce(new Vector2(0f, jumpHoldForce), ForceMode2D.Impulse);
            }

            //  ..if Jump time is past
            //  ..then set isJumping to false
            if (jumpTime <= Time.time)
            {
                isJumping = false;
            }
        }

        //  If player is falling too fast,
        //  then reduce the Y velocity to the maxFallSpeed
        if (rbody.velocity.y < maxFallSpeed)
        {
            rbody.velocity = new Vector2(rbody.velocity.x, maxFallSpeed);
        }
    }

    void StandUp()
    {
        //  If the player's head is blocked
        //  then can't stand
        if (isHeadBlocked)
        {
            return;
        }

        //  Player isn't crouching
        isCrouching = false;

        //  Apply the Standing collider size and offset
        bodyCollider.size = colliderStandSize;
        bodyCollider.offset = colliderStandOffset;
    }

    void Crouch()
    {
        //  Set Player is Crouching
        isCrouching = true;

        //  Apply the Crouching Collider size and offset
        bodyCollider.size = colliderCrouchSize;
        bodyCollider.offset = colliderCrouchOffset;
    }

    void FlipCharacterDirection()
    {
        //  Flip the direction
        direction *= -1;

        //  Save the current Scale
        Vector3 scale = transform.localScale;

        //  Set the X scale to original times the direction
        scale.x = originalXScale * direction;

        //  Apply new Scale
        transform.localScale = scale;
    }

    //  Apply Modifiers
    public void ApplyModifier(ModifierList modifier, float modifierValue, float timePeriod)
    {


        StartCoroutine(Modifier(modifier, modifierValue, timePeriod));
    }

    IEnumerator Modifier(ModifierList modifier, float modifierValue, float timePeriod)
    {
        float lastValue = 0;        //  Temporary Save LastValue
        switch (modifier)
        {
            case ModifierList.speed:
                lastValue = speed;
                speed = speed * modifierValue;      //  Applied Modifier
                break;
        }

        yield return new WaitForSeconds(timePeriod);

        //  Switch back to its original value
        switch (modifier)
        {
            case ModifierList.speed:
                speed = lastValue;
                break;
        }

    }
}
