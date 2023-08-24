using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const float PLAYER_DISTANCE_SPAWN_LEVEL_PART = 200f;

    [SerializeField] private Transform testingPlatform;
    [SerializeField] private Transform levelPart_Start;
    [SerializeField] private List<Transform> levelPartEasyList;
    [SerializeField] private List<Transform> levelPartMediumList;
    [SerializeField] private List<Transform> levelPartHardList;
    [SerializeField] private List<Transform> levelPartExtremeList;

    private Vector3 lastEndPosition;

    // CHECK FOR WHEN TO INCREASE DIFFICULTY --> USING A PLATFORM COUNTER
    // --> For future, might want to use score to determine diffulty as it's distance based
    private int levelPartsSpawned;

    private enum Difficuty
    {
        Easy,
        Medium,
        Hard,
        Extreme
    }

    // NOTE:
    // CAN MAKE LEVEL PARTS MULTIPLE PLATFORMS, NOT JUST A SINGULAR ONE IN THE GAME OBJECT
    // Just make sure to add end position to the last platform in the part

    private void Awake()
    {
        lastEndPosition = levelPart_Start.Find("EndPosition").position;

        if (testingPlatform != null)
        {
            Debug.Log("Using Debug Testing Platform");
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
                GameManager.Instance.SpeedModifier = GameManager.Instance.SpeedModifierList[0];
                break;
            case Difficuty.Medium:
                difficultyLevelPartList = levelPartMediumList;
                GameManager.Instance.SpeedModifier = GameManager.Instance.SpeedModifierList[1];
                break;
            case Difficuty.Hard:
                difficultyLevelPartList = levelPartHardList;
                GameManager.Instance.SpeedModifier = GameManager.Instance.SpeedModifierList[2];
                break;
            case Difficuty.Extreme:
                difficultyLevelPartList = levelPartExtremeList;
                GameManager.Instance.SpeedModifier = GameManager.Instance.SpeedModifierList[3];
                break;             
        }

        Transform chosenLevelPart = difficultyLevelPartList[Random.Range(0, difficultyLevelPartList.Count)];

        // Allows us to test a platform in the game right at the start
        if (testingPlatform != null)
        {
            chosenLevelPart = testingPlatform;
        }

        Transform lastLevelPartTransform = SpawnLevelPart(chosenLevelPart, lastEndPosition);
        lastEndPosition = lastLevelPartTransform.Find("EndPosition").position;
        levelPartsSpawned++;
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    // BASE DIFFICULTY OFF OF DISTANCE/SCORE
    private Difficuty GetDifficulty()
    {
        if (levelPartsSpawned >= 40)
        {
            return Difficuty.Extreme;
        }
        if (levelPartsSpawned >= 30)
        {
            return Difficuty.Hard;
        }
        if (levelPartsSpawned >= 20)
        {
            return Difficuty.Medium;
        }
        return Difficuty.Easy;
    }
}
