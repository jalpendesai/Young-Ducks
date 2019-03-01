using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    public float speed;     //	Player speed

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

    //	Private Variables
    private Rigidbody2D rbody;              //	Rigidbody Component
    private BoxCollider2D bodyCollider;     //	Collider Component
    private InputManager inputManager;      //	Current Inputs for the player

    void Start()
    {
        //	Get a reference to the required components
        inputManager = GetComponent<InputManager>();
        rbody = GetComponent<Rigidbody2D>();
        bodyCollider = GetComponent<BoxCollider2D>();
    }

    void FixedUpdate()
    {
        //	Check the environment to determine status
        PhysicsCheck();

        //	Process ground
        GroundMovement();
    }

    void PhysicsCheck()
    {
        //	Assume the player isn't on ground
        isOnGround = false;

        //	Cast rays for the left and right foot
        RaycastHit2D leftCheck = Raycast(new Vector2(-footOffset, 0f), Vector2.down, groundDistance);
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
		//	Call the overloaded Raycast() method using the ground layermask
		//	and return the results
		return Raycast(offset,rayDirection, length, groundLayer);
    }

	RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask){
		//	Record the player's position
		Vector2 pos = transform.position;

		//	Send out the desired Raycast and record the result
		RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

		//	Show Debug Raycasts in the Scene
		if(drawDebugRaycasts){
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
		
    }
}
