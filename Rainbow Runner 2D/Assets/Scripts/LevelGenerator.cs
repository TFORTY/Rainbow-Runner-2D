using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartList;

    private Vector3 lastEndPosition;

    [SerializeField] private int startingSpawnLevelParts = 5;

    // NOTE:
    // CAN MAKE LEVEL PARTS MULTIPLE PLATFORMS, NOT JUST A SINGULAR ONE IN THE GAME OBJECT
    // Just make sure to add end position to the last platform in the part

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        for (int i = 0; i < startingSpawnLevelParts; ++i)
        {
            SpawnLevelPart();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(Player.Instance.transform.position, lastEndPosition) < PLAYER_DISTANCE_SPAWN_LEVEL_PART)
        {
            // Spawn another level part
            SpawnLevelPart();
        }
    }

    private void SpawnLevelPart()
    {
        Transform chosenLevelPart = levelPartList[Random.Range(0, levelPartList.Count)];
        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }
}
