using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Banana : MonoBehaviour
{   
    public int scoreGive = 100;
    //moneda
    void OnTriggerEnter2D(Collider2D collision)
    {
         if (collision.gameObject.CompareTag("Player"))
        {
            Game.obj.addScore(scoreGive);
            gameObject.SetActive(false);
        }
    }
    

}
