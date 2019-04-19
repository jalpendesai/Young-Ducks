using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrapCollider : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            Debug.Log("Dead Meat");
            AudioManager.PlayPlayerDeathAudio();
            Destroy(other.gameObject);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

        if(other.collider.CompareTag("Enemy")){
            Destroy(other.gameObject);
        }
    }
}
