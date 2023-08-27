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

    [SerializeField] private List<float> difficultyScoreThresholds;

    private Vector3 lastEndPosition;

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
    }

    private Transform SpawnLevelPart(Transform levelPart, Vector3 spawnPosition)
    {
        Transform levelPartTransform = Instantiate(levelPart, spawnPosition, Quaternion.identity);
        return levelPartTransform;
    }

    // BASE DIFFICULTY OFF OF DISTANCE/SCORE
    private Difficuty GetDifficulty()
    {
        if (Player.Instance.Distance >= difficultyScoreThresholds[2])
        {
            return Difficuty.Extreme;
        }
        if (Player.Instance.Distance >= difficultyScoreThresholds[1] && Player.Instance.Distance < difficultyScoreThresholds[2])
        {
            return Difficuty.Hard;
        }
        if (Player.Instance.Distance >= difficultyScoreThresholds[0] && Player.Instance.Distance < difficultyScoreThresholds[1])
        {
            return Difficuty.Medium;
        }
        return Difficuty.Easy;
    }
}
