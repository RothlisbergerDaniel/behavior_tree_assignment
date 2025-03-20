using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGarbage : MonoBehaviour
{
    public GameObject garbagePrefab;
    public float spawnDelay;
    public float spawnRange; //range of locations at which to spawn garbage
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;  
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnDelay)
        {
            Instantiate(garbagePrefab, new Vector3(Random.Range(-spawnRange, spawnRange), 3, Random.Range(-spawnRange, spawnRange)), Random.rotation);
            //spawn new garbage at a random location in the air

            timer = 0; //reset timer
        }
    }
}
