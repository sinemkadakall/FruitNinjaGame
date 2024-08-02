using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;


    private void Start()
    {
        newGame();
    }
    void newGame()
    {
        score = 0;
        scoreText.text = score.ToString();
    }

   public void IncreaseScore()
    {
        score++;
        scoreText.text=score.ToString();
    }


}
