using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstacles;
    public Vector3 spawnPos;
    private PlayerController playerControllerScript;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnObstacle", 2f, 2f);
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnObstacle()
    {
        int randomIndex = Random.Range(0,2);
        spawnPos = new Vector3(Random.Range(30,40), 0, 0);
        Debug.Log("Obstacle spawning at position: "+spawnPos);
        if(playerControllerScript.gameOver == false)
        {
            Instantiate(obstacles[randomIndex], spawnPos, obstacles[randomIndex].transform.rotation);
        }
    }
}
