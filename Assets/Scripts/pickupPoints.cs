using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupPoints : MonoBehaviour
{

    public int moneyToGive;

    private ScoreManager theScoreManager;

    private AudioSource coinSound;

    void Start()
    {
        theScoreManager = FindObjectOfType<ScoreManager>();

        coinSound = GameObject.Find("CoinSound").GetComponent<AudioSource>(); ;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            theScoreManager.AddMoney(moneyToGive);
            gameObject.SetActive(false);

            if (coinSound.isPlaying)
            {
                coinSound.Stop();
                coinSound.Play();
            }
            else
            {
                coinSound.Play();
            }
        }
    }

}
