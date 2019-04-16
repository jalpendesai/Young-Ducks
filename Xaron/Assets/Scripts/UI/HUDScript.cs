using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HUDScript : MonoBehaviour
{
    public Text scoreBoard;
    public Text livesLeft;

    private PlayerStats playerStats;
    private PlayerController player;
    private void Start()
    {
        playerStats = player.GetComponent<PlayerStats>();
    }
    private void Update()
    {
        // livesLeft.text = playerStats.currentHealth();
    }
}
