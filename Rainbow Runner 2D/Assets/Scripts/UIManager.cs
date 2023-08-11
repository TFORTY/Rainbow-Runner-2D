using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    Player player;
    TMP_Text scoreText;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        scoreText = GameObject.Find("Score").GetComponent<TMP_Text>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int score = Mathf.FloorToInt(player.distance);
        scoreText.text = score.ToString();
    }
}
