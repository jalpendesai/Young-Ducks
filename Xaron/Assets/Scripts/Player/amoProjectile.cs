﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class amoProjectile : MonoBehaviour
{
    public float speed = 10;
    public float lifeTime = 1.5f;
    // public float distance;
    // public int damage;
    // public LayerMask whatIsSolid;

    // public GameObject destroyEffect;

    private void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    private void Update()
    {
        // RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.up, distance, whatIsSolid);
        // if (hitInfo.collider != null) {
        //     if (hitInfo.collider.CompareTag("Enemy")) {
        //         hitInfo.collider.GetComponent<Enemy>().TakeDamage(damage);
        //     }
        //     DestroyProjectile();
        // }


        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void DestroyProjectile() {
        // Instantiate(destroyEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
