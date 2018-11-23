using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public GameObject ballTemplate;
    public GameObject ballContainer;
    public float spawnFrequency = 1;
    public float deathPlane = -10;

    GameObject[] ballSpawns;


    // Use this for initialization
    void Start()
    {
        ballSpawns = gameObject.GetChildObjects();
        Invoke("SpawnBall", spawnFrequency);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < deathPlane)
        {
            Destroy(gameObject);
        }
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
        Debug.Assert(ballSpawns != null && ballSpawns.Length > 0);

        float rand01 = Random.value;
        float randRange = rand01 * ballSpawns.Length;
        int index = Mathf.FloorToInt(randRange);
        return ballSpawns[index];
    }
}
