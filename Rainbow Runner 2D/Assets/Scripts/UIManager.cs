using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    private float score;
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text finalScoreText;

    // Update is called once per frame
    void Update()
    {
        // SCORE
        // Check is player is not dead, if not, add score
        if (!GameManager.Instance.IsGameOver)
        {
            score = Player.Instance.Distance;
            scoreText.text = score.ToString("0");
        }     
        else if (GameManager.Instance.IsGameOver)
        {           
            finalScoreText.text = score.ToString("0");
        }
    }
}
