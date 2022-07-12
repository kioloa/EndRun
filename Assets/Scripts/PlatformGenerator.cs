using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject thePlatform;
    public Transform generationPoint;
    public float distanceBetween;

    private float platformWidth;

    public float distanceBetweenMin;
    public float distanceBetweenMax;

    private int platformSelector;
    private float[] platformWidths;

    public ObjectPooler[] theObjectPools;

    private float minHeight;
    public Transform maxHeightPoint;
    private float maxHeight;
    public float maxHeightChange;
    private float heightChange;

    //Монетки
    private CoinGenerator theCoinGenerator;
    public float randomCoinThreshold;

    //Спайки
    public float randomSpikeThreshold;
    public ObjectPooler theSpikePool;

    //Призрак
    private GhostGenerator theGhostGenerator;
    public float randomGhostThreshold;

    //private Rigidbody2D ghostRigidbody;

    //Бонусы
    public float powerupHeight;
    public ObjectPooler powerupPool;
    public float powerupThreshold;

    void Start()
    {
        //ghostRigidbody = FindObjectOfType<GhostGenerator>().GetComponent<Rigidbody2D>();

        platformWidths = new float[theObjectPools.Length];

        for (int i = 0; i < theObjectPools.Length; i++)
        {
            platformWidths[i] = theObjectPools[i].pooledObject.GetComponent<BoxCollider2D>().size.x;
        }

        minHeight = transform.position.y;
        maxHeight = maxHeightPoint.position.y;

        theCoinGenerator = FindObjectOfType<CoinGenerator>();
        theGhostGenerator = FindObjectOfType<GhostGenerator>();
    }

    void Update()
    {

        if (transform.position.x < generationPoint.position.x)
        {
            distanceBetween = Random.Range(distanceBetweenMin, distanceBetweenMax);

            platformSelector = Random.Range(0, theObjectPools.Length);

            heightChange = transform.position.y + Random.Range(maxHeightChange, -maxHeightChange);

            if (heightChange > maxHeight)
            {
                heightChange = maxHeight;
            } 
            else if (heightChange < minHeight)
            {
                heightChange = minHeight;
            }

            if(Random.Range(0f, 100f) < powerupThreshold)
            {
                GameObject newPowerup = powerupPool.GetPooledObject();

                newPowerup.transform.position = transform.position + new Vector3(distanceBetween / 2, Random.Range(1f, powerupHeight), 0f);

                newPowerup.SetActive(true);
            }


            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2)  + distanceBetween, heightChange, transform.position.z);
            
            GameObject newPlatform = theObjectPools[platformSelector].GetPooledObject();

            newPlatform.transform.position = transform.position;
            newPlatform.transform.rotation = transform.rotation;
            newPlatform.SetActive(true);

            if (Random.Range(0f, 100f) < randomCoinThreshold)
            {
                theCoinGenerator.SpawnCoins(new Vector3(transform.position.x + 1f, transform.position.y + 1f, transform.position.z));
            }

            if (Random.Range(0f, 100f) < randomGhostThreshold)
            {
                if (platformWidths[platformSelector] > 4f)
                {
                    Vector3 startPosition = new Vector3(transform.position.x, transform.position.y + Random.Range(1f, 3f), transform.position.z);
                    theGhostGenerator.SpawnGhosts(startPosition);
                }
            }

            if (Random.Range(0f, 100f) < randomSpikeThreshold)
            {
                if (platformWidths[platformSelector] > 3f)
                {
                    GameObject newSpike = theSpikePool.GetPooledObject();

                    float spikeXPosition = Random.Range(-platformWidths[platformSelector] / 2f + 2f, platformWidths[platformSelector] / 2f - 1f);

                    Vector3 spikePosition = new Vector3(spikeXPosition, 0.5f, 0f);

                    newSpike.transform.position = transform.position + spikePosition;
                    newSpike.transform.rotation = transform.rotation;
                    newSpike.SetActive(true);
                }
            }

            transform.position = new Vector3(transform.position.x + (platformWidths[platformSelector] / 2), transform.position.y, transform.position.z);
        }
    }
}
