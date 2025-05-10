using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{

    [SerializeField] List<GameObject> obstacles;

    [SerializeField] Transform spawnPointL;
    [SerializeField] Transform spawnPointR;
    [SerializeField] float spawnRange = 6;
    [SerializeField] float spawnTime = 10;
    [SerializeField] float timeDecreaseRate = 0.05f;

    float spawnTimer;

    void Start()
    {
        spawnTimer = 0;
    }

    void Update()
    {
        if (spawnTimer > spawnTime) 
        {
            SpawnObstacle();

            spawnTimer = 0;
            spawnTime -= timeDecreaseRate;
        }
        spawnTimer += Time.deltaTime;
    }

    private void SpawnObstacle()
    {

        GameObject newObstacle;

        if (Random.Range(0, 2) == 1) newObstacle = Instantiate(
            obstacles[Random.Range(0, obstacles.Count)],
            new Vector3(
                spawnPointL.transform.position.x,
                Random.Range(spawnPointL.transform.position.y - spawnRange, spawnPointL.transform.position.y + spawnRange),
                spawnPointL.transform.position.z
                ),
            spawnPointL.rotation, 
            spawnPointL
            );

        else newObstacle = Instantiate(
            obstacles[Random.Range(0, obstacles.Count)], 
            new Vector3(
                spawnPointR.transform.position.x,
                Random.Range(-spawnRange, spawnRange),
                spawnPointR.transform.position.z
                ), 
            spawnPointR.rotation, 
            spawnPointR
            );

    }

}
