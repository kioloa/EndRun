using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GhostGenerator : MonoBehaviour
{
    public ObjectPooler ghostPool;
  
    public void SpawnGhosts(Vector3 startPosition)
    {
        GameObject ghost = ghostPool.GetPooledObject();
        ghost = ghostPool.GetPooledObject();
        ghost.transform.position = startPosition;
        ghost.SetActive(true);
    }

}


