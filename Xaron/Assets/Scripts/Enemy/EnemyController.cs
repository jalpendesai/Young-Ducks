using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    //  Start   ->  Patrol

    //  if player collides with eyeSight   ->  follow & ShootAtPlayer()

    //  if player is not in sight   ->  Patrol

    public float patrolSpeed = 2.0f;
    public float eyeHeight = 1.0f;
    public float fovAngle = 60f;

    //  Enemy Shooting  
    public GameObject bullet;
    public float timeBetweenShots = 0.3f;
    public float startTimeBetweenShots;

    //  Private Variables
    private int dir = -1;
    private bool isMovingLeft = true;
    private Transform groundDetection;
    private bool canShoot = false;
    private bool isPatrolling = true;


    private void Start()
    {
        // groundDetection = GameObject.Find("GroundDetection").transform.GetChild(0);
        groundDetection = gameObject.transform.GetChild(0);
        // fovAngle = fovAngle * Mathf.Deg2Rad;
    }

    private void FixedUpdate()
    {
        EnemyPatrol();
        EyeSight();
    }
    private void EnemyPatrol()
    {
        //  Starts moving left
        if (isPatrolling)
        {
            transform.Translate(Vector2.left * patrolSpeed * Time.deltaTime);
        }

        //  Check if enemy reaches the patrol edge
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, 2.0f);
        if (groundInfo.collider == false)
        {
            if (isMovingLeft == true)
            {
                transform.eulerAngles = new Vector3(0, -180f, 0);
                isMovingLeft = false;
                dir = 1;
            }
            else
            {
                transform.eulerAngles = Vector3.zero;
                isMovingLeft = true;
                dir = -1;
            }
        }
    }

    //  Check if player is in EyeSight 
    private void EyeSight()
    {
        //  Shoot three Raycasts
        //  If collides with player then shoot



        // Vector3 eyePosition = new Vector3(transform.position.x, eyeHeight, transform.position.z);

        // Vector3 rayDirection = new Vector3(0,1,0);

        // Vector3 leftRay = Quaternion.AngleAxis(-15, rayDirection) * transform.forward;
        // Vector3 rightRay = Quaternion.AngleAxis(fovAngle, rayDirection) * transform.forward;

        // RaycastHit2D hit = Physics2D.Raycast(this.transform.position, leftRay, 10);
        // // RaycastHit2D hit2 = Physics2D.Raycast(this.transform.position, rightRay, 10);
        // // RaycastHit2D hit3 = Physics2D.Raycast(this.transform.position, transform.forward, 10);
        // Color color = Color.red;
        // Debug.DrawRay(this.transform.position, leftRay * 10, color);
        // // Debug.DrawRay(this.transform.position, rightRay  * 10, color);
        // // Debug.DrawRay(this.transform.position, transform.forward  * 10, color);

        // // Vector2 pos = this.transform.position;
        // // Vector3 direction = new Vector3(Mathf.Sin(fovAngle),Mathf.Cos(fovAngle), 0);
        // // Vector3 direction2 = new Vector3(Mathf.Sin(-fovAngle),Mathf.Cos(-fovAngle), 0);

        // // RaycastHit2D hit = Physics2D.Raycast(pos, direction * dir, 5);
        // // RaycastHit2D hit2 = Physics2D.Raycast(pos, direction2 * dir, 5);

        // if (hit.collider!= null && hit.collider.gameObject.name == "Player")
        // {
        //     Debug.Log("Player detected");
        // }
        // // Color color = Color.red;
        // // Debug.DrawRay(pos, direction * dir * 5, color);
        // // Debug.DrawRay(pos, direction2 * dir * 5, color);



    }

    private void Update()
    {
        if (canShoot)
        {
            CheckIfTimeToShoot();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player detected");
            isPatrolling = false;
            canShoot = true;

        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPatrolling = false;
            canShoot = true;
        }
    }
    private void CheckIfTimeToShoot()
    {
        if (timeBetweenShots <= 0)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        isPatrolling = true;
        canShoot = false;
    }

}
