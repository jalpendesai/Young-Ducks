using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class patrol : MonoBehaviour
{
    public float speed;
    public Transform groundDetaction;

    private bool movingRight = true;

    private void Update() {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetaction.position,Vector2.down,2f);

        if(groundInfo.collider == false){
            if(movingRight == true){
                transform.eulerAngles = new Vector3(0,-180f,0);
                movingRight = false;  
            }
            else
            {
                transform.eulerAngles = new Vector3(0,0,0);
                movingRight = true;
            }
        }
    }
}
