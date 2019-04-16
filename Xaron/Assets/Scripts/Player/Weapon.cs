using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector2 velocity;
    public GameObject ammoPrefab;
    // public GameObject shotEffect;
    public Vector2 offset = new Vector2(0.6f, 0.1f);
    // public Animator camAnim;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private void Start()
    {
    }
    // public GameObject weapon;

    private void Update()
    {
        // Handles the weapon rotation
        // Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (timeBtwShots <= 0)
        {
            if (Input.GetButtonDown("Fire"))
            {
                // Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
                // camAnim.SetTrigger("shake");
                GameObject bullet = (GameObject)Instantiate(ammoPrefab, (Vector2)transform.position + offset * transform.localScale.x, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
                Destroy(bullet,1.0f);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

}
