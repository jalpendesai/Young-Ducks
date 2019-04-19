using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HUDScript : MonoBehaviour
{
    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI livesLeft;
    private PlayerStats playerStats;

    private static HUDScript instance;
    public int score = 0;
    public int lives = 3;
    public int playerHP = 100;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // playerStats = player.GetComponent<PlayerStats>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }
    private void Update()
    {
        // livesLeft.text = playerStats.currentHealth();
        instance.livesLeft.text = "Health: " + instance.playerHP.ToString();
        instance.scoreBoard.text = "Score: " + instance.score.ToString();
    }
}
