using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] GameObject obstacle;
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;

    [SerializeField] float timeBetweenSpawns;
    private float spawnTime;

    // Update is called once per frame
    void Update()
    {
        // NEED TO ONLY SPAWN WHEN ENTER IS PRESSED
        // DESTROY OBSTACLES THAT GO OFF SCREEN

        transform.position += new Vector3(GameManager.Instance.CameraSpeed * Time.deltaTime, 0, 0) * GameManager.Instance.SpeedModifier;

        if (Time.time > spawnTime)
        {
            SpawnObstacle();
            spawnTime = Time.time + timeBetweenSpawns;
        }
    }

    void SpawnObstacle()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);

        Instantiate(obstacle, transform.position + new Vector3(randomX, randomY, 0), transform.rotation);
    }
}
