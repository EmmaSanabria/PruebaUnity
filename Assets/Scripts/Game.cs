using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game obj;

    public int maxLives = 3;

    public bool gamePaused = false;
    public int score = 0;

    void Awake()
    {
        obj = this;
    }
    void Start()
    {
        gamePaused = false;
    }

    
    public void addScore(int points)
    {
        score += points;
    }


    public void gameOver()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDestroy()
    {
        obj = null;
    }
}
