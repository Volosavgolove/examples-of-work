using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private float spawnRangeX = 10f;
    private float spawnPosZ = 30f;
    public GameObject[] animalPrefabs;
    public float startingIn = 2f;
    public float spawninterval = 3f;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnMethod", startingIn, spawninterval);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void SpawnMethod()
    {
        Vector3 SpawnPos = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 0, spawnPosZ);
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Instantiate(animalPrefabs[animalIndex], SpawnPos, animalPrefabs[animalIndex].transform.rotation);
    }
}
