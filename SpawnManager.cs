using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PlayerController playerControlerScript;
    private float startDelay = 2;
    private float spawnInterval = 2;
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0.5f, 0);
    // Start is called before the first frame update
    void Start()
    {
        playerControlerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (playerControlerScript.gameOver == false)
        {
            Instantiate(obstaclePrefab, spawnPos, obstaclePrefab.transform.rotation);
        }
    }
}
