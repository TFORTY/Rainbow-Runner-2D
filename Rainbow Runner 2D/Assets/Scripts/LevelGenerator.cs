using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;

    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartEasyList;
    [SerializeField] private List<Transform> levelPartMediumList;
    [SerializeField] private List<Transform> levelPartHardList;
    [SerializeField] private List<Transform> levelPartExtremeList;

    private Vector3 lastEndPosition;

    // CHECK FOR WHEN TO INCREASE DIFFICULTY --> USING A PLATFORM COUNTER
    private int levelPartsSpawned;

    private enum Difficuty
    {
        Easy,
        Medium,
        Hard,
        Extreme
    }

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
        List<Transform> difficultyLevelPartList;
        switch(GetDifficulty())
        {
            default:
            case Difficuty.Easy:
                difficultyLevelPartList = levelPartEasyList;
                break;
            case Difficuty.Medium:
                difficultyLevelPartList = levelPartMediumList;
                break;
            case Difficuty.Hard:
                difficultyLevelPartList = levelPartHardList;
                break;
            case Difficuty.Extreme:
                difficultyLevelPartList = levelPartExtremeList;
                break;             
        }

        Transform chosenLevelPart = difficultyLevelPartList[Random.Range(0, difficultyLevelPartList.Count)];

        //// Select EASY Level Part
        //chosenLevelPart = levelPartEasyList[Random.Range(0, levelPartEasyList.Count)]; 
        //// Select MEDIUM Level Part
        //if (levelPartsSpawned > 5)
        //{
        //    chosenLevelPart = levelPartMediumList[Random.Range(0, levelPartMediumList.Count)];
        //}

        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        levelPartsSpawned++;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    private Difficuty GetDifficulty()
    {
        if (levelPartsSpawned >= 30)
        {
            return Difficuty.Extreme;
        }
        if (levelPartsSpawned >= 20)
        {
            return Difficuty.Hard;
        }
        if (levelPartsSpawned >= 10)
        {
            return Difficuty.Medium;
        }
        return Difficuty.Easy;
    }
}
