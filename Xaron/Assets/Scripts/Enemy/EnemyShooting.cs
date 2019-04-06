using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public bool isAttacking = false;
    // public GameObject _bullet;
    // public Transform bulletSpawnPoint;
    // public float fireRate;
    // public float nextfire;
    public float sightDistance = 5.0f;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        CheckPlayer();      // Check for player
    }

    //  Check for player
    void CheckPlayer()
    {
        //  Raycast2d, if hit with player then Attack and Stop Patrolling
        //  else patroll and checkplayer()
        Vector2 pos = this.transform.position;
        Vector2 direction = new Vector2(1,0);
        RaycastHit2D hit = Physics2D.Raycast(pos, direction,sightDistance);
        if (hit.collider != null)
        {
            Debug.Log("Player detected");
        }
        Color color = Color.red;
        Debug.DrawRay(pos, direction * sightDistance, color);

    }
}
