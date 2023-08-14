using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform levelPart_1;

    private void Awake()
    {
        SpawnLevelPart(new Vector3(36, -11));
        SpawnLevelPart(new Vector3(36, -11) + new Vector3(20, 0));
        SpawnLevelPart(new Vector3(36, -11) + new Vector3(20 + 20, 0));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnLevelPart(Vector3 spawnPosition)
    {
        Instantiate(levelPart_1, spawnPosition, Quaternion.identity);
    }
}
