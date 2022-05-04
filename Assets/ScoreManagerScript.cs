using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManagerScript : MonoBehaviour
{
    public GameObject youWin;
    public Text scoreText;
    // Start is called before the first frame update
    int score;
    public void Score(int scoreValue)
    {

         score = score + scoreValue;
        Debug.Log(score);
        scoreText.text = score.ToString();
        if(score > 100)
        {
            youWin.SetActive(true);
        }

    }
}
