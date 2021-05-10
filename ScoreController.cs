using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    public int score;

    private Text myText;

    private void Start()
    {
        myText = GetComponent<Text>();
        resetScore();
        
    }
    public void increaseScore(int point) // when my enemy dies the score will increase.
    {
        score += point;
        myText.text = score.ToString();
    }

    public void resetScore()
    {
        score = 0;
        myText.text = score.ToString();
    }

    // Now, go to EnemyController Script.
}
