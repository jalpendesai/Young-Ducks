using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amoProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifeTime = 1.5f;
    public int damage = 30;
    // public float distance;
    // public LayerMask whatIsSolid;

    // public GameObject destroyEffect;

    private void Update()
    {
        // RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        // if (hitInfo.collider != null) {
        //     if (hitInfo.collider.CompareTag("Enemy")) {
        //         hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
        //     }
        //     DestroyProjectile();
        // }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
        EnemyController enemy = other.gameObject.GetComponent<EnemyController>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
            AudioManager.PlayEnemyHitAudio();            
        }


    }
}
