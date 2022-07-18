using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ShowRecord : MonoBehaviour
{

    private ScoreManager theScoreManager;

    public Text BestResult;

    private float BestScore;

    //private ScoreManager temp;

    void Start()
    {

        
        theScoreManager = FindObjectOfType<ScoreManager>();

        BestScore = theScoreManager.highScoreCount;

        BestResult.text = "" + BestScore;

        //temp = theScoreManager;

        //Destroy(theScoreManager);

        //theScoreManager = temp;
        
        theScoreManager.enabled = false;
        
    }

}
