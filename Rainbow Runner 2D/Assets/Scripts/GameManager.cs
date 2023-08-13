using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // TODO
    // Platform speeds
    // Animators
    // Obstacle stuff (speed, etc)
    // Powerup stuff
    // HUD references
    // Deal with game starting, losing, restarting
    // Audio Sources, Game Music, Game Ambience

    public static GameManager Instance { get; private set; }

    [SerializeField] float cameraSpeed;
    [SerializeField] Transform cameraTransform;
    [SerializeField] float speedModifier = 1;
    [SerializeField] float deathYPos = -18;

    [SerializeField] GameObject gameOverPanel;
    private bool isGameOver = false;
    [SerializeField] GameObject inGameScore;

    private bool startGame = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        isGameOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        GameOver();

        if (!startGame)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                startGame = true;
            }
        }
    }

    private void LateUpdate()
    {
        if (startGame)
        {
            // Move the camera in the x direction
            cameraTransform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0) * speedModifier;
        }       
    }  

    public float GetCameraSpeed()
    {
        return cameraSpeed;
    }

    public float GetSpeedModifier()
    {
        return speedModifier;
    }

    public void SetSpeedModifier(float newValue)
    {
        speedModifier = newValue;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        if (Player.Instance.transform.position.y <= deathYPos)
        {
            gameOverPanel.SetActive(true);
            cameraSpeed = 0;
            isGameOver = true;
            inGameScore.SetActive(false);
        }
    }

    public bool GetIsGameOver()
    {
        return isGameOver;
    }

    public bool GetStartGame()
    {
        return startGame;
    }
}
