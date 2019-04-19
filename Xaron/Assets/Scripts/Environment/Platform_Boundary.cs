using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Platform_Boundary : MonoBehaviour
{
    public float deadZone = -8f;

    public bool isEnemy = true;

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < deadZone)
        {
            if (!isEnemy)
            {
                AudioManager.PlayPlayerDeathAudio();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
    }
}
