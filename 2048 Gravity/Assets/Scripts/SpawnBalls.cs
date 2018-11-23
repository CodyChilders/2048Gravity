using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public GameObject ballTemplate;
    public GameObject ballContainer;
    public float spawnFrequency = 1;
    
    GameObject[] ballSpawns;

    void Start()
    {
        ballSpawns = gameObject.GetChildObjects();
        if(ballSpawns == null || ballSpawns.Length == 0)
        {
            throw new MissingObjectException("Cannot locate spawn points.");
        }
        Invoke("SpawnBall", spawnFrequency);
    }

    void Update()
    {
        
    }

    void SpawnBall()
    {
        GameObject chosenSpawn = SelectSpawn();
        GameObject newBall = Instantiate(ballTemplate);
        newBall.transform.position = JitterPosition(chosenSpawn.transform.position);
        newBall.transform.parent = ballContainer.transform;

        //chain this into the next ball spawning
        Invoke("SpawnBall", spawnFrequency);
    }

    /// <summary>
    /// Because we're so precise in our calculations, balls on a flat platform frequently balance atop one another.
    /// This seems unrealistic. Use this function to get a tiny variation in position 
    /// </summary>
    /// <returns>A position very slightly changed from where it was.</returns>
    Vector3 JitterPosition(Vector3 originalPosition)
    {
        Vector3 unit = Random.insideUnitSphere;
        const float dampen = 0.05f;
        return originalPosition + unit * dampen;
    }

    GameObject SelectSpawn()
    {
        float rand01 = Random.value;
        float randRange = rand01 * ballSpawns.Length;
        int index = Mathf.FloorToInt(randRange);
        return ballSpawns[index];
    }
}
