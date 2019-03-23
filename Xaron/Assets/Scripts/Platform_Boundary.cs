using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform_Boundary : MonoBehaviour
{
    public float deadZone = -8f;

    public Transform lastCheckPoint = null; 

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y < deadZone){
            this.transform.position = lastCheckPoint.position;
        }
    }
}
