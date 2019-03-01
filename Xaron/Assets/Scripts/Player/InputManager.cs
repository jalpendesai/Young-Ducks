using UnityEngine;

//	This script runs before all other player scripts to prevent laggy input
[DefaultExecutionOrder(-100)]
public class InputManager : MonoBehaviour
{

    public float horizontal;

    private bool readyToClear;

    void Update()
    {
        //	Clear out existing input values
        ClearInput();

        //	Process Keyboard, mouse, gamepad inputs
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
    }

    void ProcessInputs()
    {
        horizontal += Input.GetAxis("Horizontal");
    }
}
