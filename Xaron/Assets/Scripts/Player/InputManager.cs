using UnityEngine;

//	This script runs before all other player scripts to prevent laggy input
[DefaultExecutionOrder(-100)]
public class InputManager : MonoBehaviour
{

    [HideInInspector] public float horizontal;
    [HideInInspector] public bool crouchPressed;
    [HideInInspector] public bool crouchHeld;
    [HideInInspector] public bool jumpPressed;
    [HideInInspector] public bool jumpHeld;
    private bool readyToClear;

    void Update()
    {
        //	Clear out existing input values
        ClearInput();

        //  If Game Over then exit
        // if (GameManager.IsGameOver())
        // {
        //     return;
        // }

        //	Process Keyboard, mouse
        ProcessInputs();

        //	Clamp horizontal input to between -1 and 1
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    }

    void FixedUpdate()
    {
        //	Set a flag that lets inputs to be cleared out during the
        //	next Update(). This ensures that all code gets to use the current inputs.
        readyToClear = true;
    }

    void ClearInput()
    {
        //	If we're not ready to clear input, exit
        if (!readyToClear)
        {
            return;
        }

        //	Reset all input values
        horizontal = 0.0f;
        jumpPressed = false;
        jumpHeld = false;
        crouchPressed = false;
        crouchHeld = false;
        readyToClear = false;
    }

    void ProcessInputs()
    {
        //  Horizontal Input Axis
        horizontal += Input.GetAxis("Horizontal");

        //  Jump
        jumpPressed = jumpPressed || Input.GetButtonDown("Jump");
        jumpHeld = jumpHeld || Input.GetButton("Jump");

        //  Crouch
        // crouchPressed = crouchPressed || Input.GetButtonDown("Crouch");
        // crouchHeld = crouchHeld || Input.GetButton("Crouch");
        if(Input.GetAxis("Crouch") > 0){
            crouchHeld = crouchPressed = true;
        }
    }
}
