using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    private GameManager _gameManager;
    public Vector2 nextScenePos;
    private void Start() {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            // _gameManager.lastCheckPointPos = nextScenePos;
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
