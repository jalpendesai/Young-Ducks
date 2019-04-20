using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ModifierList
{
    speed,
    fireShots,
    fireRegTime,
    AmmoCapacity,
    damage
};
public class PowerUpMagnet : MonoBehaviour
{
    public ModifierList modifier;
    public float speedMultiplier = 1.0f;
    public float fireShots = 1;
    public float regFireMultiplier = 1.0f;
    public float ammoCapacity = 1.0f;
    public float damageMultiplier = 1.0f;
    public float timePeriod = 1.0f;

    Rigidbody2D rb;
    GameObject player;
    Vector2 _playerDirection;
    float _timeStamp;
    bool _attractPlayer;
    PlayerController playerController;
    Weapon playerWeapon;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerWeapon = GameObject.FindGameObjectWithTag("Player").GetComponent<Weapon>();

    }

    // Update is called once per frame
    void Update()
    {

        // Attrack to Player Like MAGNET
        if (_attractPlayer)
        {
            _playerDirection = -(transform.position - player.transform.position).normalized;
            rb.velocity = new Vector2(_playerDirection.x, _playerDirection.y) * 10f * (Time.time / _timeStamp);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "PowerUpMagnet")
        {
            _timeStamp = Time.time;
            player = GameObject.Find("Player");
            _attractPlayer = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);

            //  Apply Modifier

            switch (modifier)
            {
                case ModifierList.speed:
                    playerController.ApplyModifier(modifier, speedMultiplier, timePeriod);
                    break;

                case ModifierList.fireShots:
                    playerWeapon.ApplyModifier(modifier, fireShots, timePeriod);
                    break;

                case ModifierList.fireRegTime:
                    playerWeapon.ApplyModifier(modifier, regFireMultiplier, timePeriod);
                    break;

                case ModifierList.AmmoCapacity:
                    playerWeapon.ApplyModifier(modifier, ammoCapacity, timePeriod);                
                    break;

                case ModifierList.damage:
                    break;
            }

        }
    }



}
