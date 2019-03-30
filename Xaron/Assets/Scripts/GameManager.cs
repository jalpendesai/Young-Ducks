using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Vector2 lastCheckPointPos;
    private static GameManager instance;

    //  AWAKE will be called before START 
    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
