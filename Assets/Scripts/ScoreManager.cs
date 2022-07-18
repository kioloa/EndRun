using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{

    public Text scoreText;
    public Text highScoreText;
    public Text moneyText;

    public float scoreCount; //����
    public float highScoreCount; //������
    public float moneyCount; //������

    public float pointsPerSecond;

    public bool scoreIncreasing;

    public bool shouldDouble;

    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("HighScore");
        }     
    }

    void Update()
    {
        if (scoreIncreasing)
        {
            scoreCount += pointsPerSecond * Time.deltaTime;
        }
        if (scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("HighScore", highScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
        moneyText.text = "Money: " + Mathf.Round(moneyCount);
    }

    public void AddScore(int pointsToAdd)
    {
        if (shouldDouble)
        {
            pointsToAdd = pointsToAdd * 2;
        }
        scoreCount += pointsToAdd;
    }

    public void AddMoney(int moneyToAdd)
    {
        moneyCount += moneyToAdd;
    }

    public bool PowerUp1() //������
    {
        if (moneyCount - 20 >= 0)
        {
            moneyCount -= 20;
            return true;
        }
        else
            return false;
    }

}
