using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpMagnet : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    Vector2 _playerDirection;
    float _timeStamp;
    bool _attractPlayer;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        // Attrack to Player Like MAGNET
        if (_attractPlayer)
        {
            _playerDirection = -(transform.position - player.transform.position).normalized;
            rb.velocity = new Vector2(_playerDirection.x, _playerDirection.y) * 10f * (Time.time / _timeStamp);
        }
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "PowerUpMagnet")
        {
            _timeStamp = Time.time;
            player = GameObject.Find("Player");
            _attractPlayer = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
