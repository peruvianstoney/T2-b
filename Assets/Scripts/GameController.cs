using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    //public Text scoreText;
    //public Text lifesText;
    
    private int _score = 0;
    private int _lifes = 5;

    private void Start()
    {
        //scoreText.text = "Score: " + _score;
        PrintLifes();
    }


    public int GetScore()
    {
        return _score;
    }

    public void PlusScore(int score)
    {
        _score += score;
        //scoreText.text = "Score: " + _score;
    }

    public void LoseLife()
    {
        _lifes -= 1;
        PrintLifes();
    }

    public int GetLifes()
    {
        return _lifes;
    }

    private void PrintLifes()
    {
        var text = "Lives: ";
        for (var i = 0; i < _lifes; i++)
        {
            text += "I ";
        }
        
        //lifesText.text = text;
    }
}
