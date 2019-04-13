using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;

    public GameObject ammoPrefab;
    // public GameObject shotEffect;
    public Transform shotPoint;
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
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                // Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
                // camAnim.SetTrigger("shake");
                Instantiate(ammoPrefab, shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

}
