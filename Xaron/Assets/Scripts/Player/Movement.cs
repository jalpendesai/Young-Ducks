using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour {

    //  Public


    //  Private
    [SerializeField]                  private float      jumpForce       = 400f;
    [SerializeField] [Range(0,1)]     private float      crouchSpeed     = 0.36f;
    [SerializeField] [Range(0, 0.3f)] private float      smoothMovement  = 0.05f;
    [SerializeField]                  private bool       canAirControl      = false;
    [SerializeField]                  private LayerMask  whatIsGround;
    [SerializeField]                  private Transform  groundCheck;
    [SerializeField]                  private Transform  ceilingCheck;
    [SerializeField]                  private Collider2D crouchCollider;

    private const float groundedRadius = 0.2f;
    private bool        isGrounded;
    private const float ceilingRadius  = 0.2f;
    private Rigidbody2D rbody;
    private bool        facingRight    = true;
    private Vector3     velocity       = Vector3.zero;

    [Header("Events")]
    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent: UnityEvent<bool> { }

    public BoolEvent OnCrouchEvent;
    private bool wasCrouching = false;


    //  Awake will run before START funciton
    private void Awake()
    {
        rbody = GetComponent<Rigidbody2D>();

        if(OnLandEvent == null)
        {
            OnLandEvent = new UnityEvent();
        }

        if(OnCrouchEvent == null)
        {
            OnCrouchEvent = new BoolEvent();
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = isGrounded;
        isGrounded = false;

        //  Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if(colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
                if (wasGrounded)
                {
                    OnLandEvent.Invoke();
                }
            }
        }
    }

    public void Move(float move, bool crouch, bool jump)
    {
        //  if player is not crouching, then player can stand up
        if (!crouch)
        {
            //  If there's ceiling on top then player cannot stand and is crouching
            if (Physics2D.OverlapCircle(ceilingCheck.position, ceilingRadius, whatIsGround))
            {
                crouch = true;
            }
        }

        //  if player is Grounded or can do AirControl, then playerController is enabled
        if(isGrounded || canAirControl)
        {
            //  Check if player is crouching
            if (crouch)
            {
                if (!wasCrouching)
                {
                    wasCrouching = true;
                    OnCrouchEvent.Invoke(true);
                }

                //  reduce the player movement speed by crouchMultiplier
                move *= crouchSpeed;

                //  Disable the Crouching collider when crouching
                if (crouchCollider != null)
                {
                    crouchCollider.enabled = false;
                }

            }
            else
            {
                //  Enable the Crouching collider when not crouching
                if (crouchCollider != null)
                {
                    crouchCollider.enabled = true;
                }

                if (wasCrouching)
                {
                    wasCrouching = false;
                    OnCrouchEvent.Invoke(false);
                }

            }

            //  Move the player by calculating the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, rbody.velocity.y);

            //
        }
    }


}
