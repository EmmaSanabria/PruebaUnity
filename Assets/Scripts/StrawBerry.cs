using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrawBerry : MonoBehaviour
{
    public int scoreGive = 30;
    //Corazon
    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
            Game.obj.addScore(scoreGive);
            Player.obj.addLife();
            
            gameObject.SetActive(false);
        }
    }
    public void addLife();
    {
        lives++;
        if (lives > Game.obj.maxLives)
        {
            lives = Game.obj.maxLives;
        }
    }

}
