using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Vector2 lastCheckPointPos;

    [Header("PowerUps")]
    public GameObject[] powerUps;
    private static GameManager instance;

    //  AWAKE will be called before START 
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

    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            lastCheckPointPos = new Vector2(26, 2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        if (Input.GetButtonDown("Back"))
        {
            lastCheckPointPos = new Vector2(-10, -2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
        if(Input.GetButtonDown("Esc")){
            SceneManager.LoadScene(0);
        }
    }
}
