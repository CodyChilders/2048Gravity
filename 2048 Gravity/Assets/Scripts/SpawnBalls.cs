using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
    public GameObject ballTemplate;
    public float spawnFrequency = 1;

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

    }

    void SpawnBall()
    {
        GameObject chosenSpawn = SelectSpawn();
        GameObject newBall = Instantiate(ballTemplate);
        newBall.transform.position = chosenSpawn.transform.position;

        //chain this into the next ball spawning
        Invoke("SpawnBall", spawnFrequency);
    }

    GameObject SelectSpawn()
    {
        Debug.Assert(ballSpawns != null);
        Debug.Assert(ballSpawns.Length > 0);

        float rand01 = Random.value;
        float randRange = rand01 * ballSpawns.Length;
        int index = Mathf.FloorToInt(randRange);
        return ballSpawns[index];
    }
}
