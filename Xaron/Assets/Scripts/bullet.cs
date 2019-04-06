using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float moveSpeed = 7f;

    private Rigidbody2D rbody;

    // public PlayerController target;
    private Transform player;
    private Vector2 target;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        target = new Vector2(player.position.x, player.position.y);
    }

    private void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target,moveSpeed*Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y){
            DestroyBullet();
        }
    }

    void DestroyBullet(){
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Hit");
            DestroyBullet();
        }
    }
}
