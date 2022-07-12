using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMove : MonoBehaviour
{
    private float ghostSpeed;
    private Vector2 startPosition;

    public GameObject theGhost;
    
    private Rigidbody2D ghostRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        ghostRigidbody = GetComponent<Rigidbody2D>();
        startPosition = theGhost.transform.position;
        ghostSpeed = Random.Range(-1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        ghostRigidbody.velocity = new Vector2(ghostSpeed, ghostRigidbody.velocity.y);
    }
}
