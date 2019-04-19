using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector2 velocity;
    public GameObject ammoPrefab;
    // public GameObject shotEffect;
    public Vector2 offset;
    // public Animator camAnim;

    private float timeBtwShots;
    public float fireRate;
    private int ammoCapacity = 6;
    private int ammoCount = 0;
    private bool canFire = true;
    private float reloadingTime = 5.0f;
    private float fireShots = 2;

    private void Update()
    {
        // Handles the weapon rotation
        // Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        // float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0f, 0f, rotZ);

        // offset = new Vector2(0.6f, 0.6f * transform.localScale.x);

        if (ammoCount >= ammoCapacity)
        {
            //  Reload Ammo
            canFire = false;
            StartCoroutine(Reloading(reloadingTime));
        }


        if (timeBtwShots <= 0)
        {
            if (canFire && Input.GetButtonDown("Fire"))
            {
                // Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
                // camAnim.SetTrigger("shake");
                if (fireShots == 1)
                {
                    GameObject bullet = (GameObject)Instantiate(ammoPrefab, (Vector2)transform.position + offset, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);
                    Destroy(bullet, 1.0f);
                }

                if (fireShots == 2)
                {
                    GameObject bullet = (GameObject)Instantiate(ammoPrefab, (Vector2)transform.position + offset, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y + 0.5f);

                    GameObject bullet2 = (GameObject)Instantiate(ammoPrefab, (Vector2)transform.position + offset, Quaternion.identity);
                    bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y - 0.5f);

                    Destroy(bullet, 1.0f);
                    Destroy(bullet2, 1.0f);
                }

                if (fireShots == 3)
                {
                    GameObject bullet = (GameObject)Instantiate(ammoPrefab, (Vector2)transform.position + offset, Quaternion.identity);
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y + 0.5f);

                    GameObject bullet2 = (GameObject)Instantiate(ammoPrefab, (Vector2)transform.position + offset, Quaternion.identity);
                    bullet2.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y);

                    GameObject bullet3 = (GameObject)Instantiate(ammoPrefab, (Vector2)transform.position + offset, Quaternion.identity);
                    bullet3.GetComponent<Rigidbody2D>().velocity = new Vector2(velocity.x * transform.localScale.x, velocity.y - 0.5f);

                    Destroy(bullet, 1.0f);
                    Destroy(bullet2, 1.0f);
                    Destroy(bullet3, 1.0f);

                }
                timeBtwShots = fireRate;
                ammoCount++;
                Debug.Log(ammoCount);
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

    }

    IEnumerator Reloading(float reloadingTime)
    {
        ammoCount = 0;
        canFire = false;
        yield return new WaitForSeconds(reloadingTime);     // wait for reloading time
        ammoCount = 0;      //  reset counter
        canFire = true;
    }

    //  Apply Modifiers
    public void ApplyModifier(ModifierList modifier, float modifierValue, float timePeriod)
    {
        StartCoroutine(Modifier(modifier, modifierValue, timePeriod));
    }

    IEnumerator Modifier(ModifierList modifier, float modifierValue, float timePeriod)
    {
        float lastValue = 0;        //  Temporary Save LastValue
        switch (modifier)
        {
            case ModifierList.fireRegTime:
                lastValue = fireRate;
                fireRate = fireRate / modifierValue;      //  Applied Modifier
                break;

            case ModifierList.fireShots:
                lastValue = fireShots;
                fireShots = modifierValue;
                break;
        }

        yield return new WaitForSeconds(timePeriod);

        //  Switch back to its original value
        switch (modifier)
        {
            case ModifierList.fireRegTime:
                fireRate = lastValue;
                break;

            case ModifierList.fireShots:
                fireShots = lastValue;
                break;
        }

    }

}
