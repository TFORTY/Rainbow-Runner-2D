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
    // Audio Sources, Game Music, Game Ambience

    public static GameManager Instance { get; private set; }

    [SerializeField] float cameraSpeed;
    public float CameraSpeed { get { return cameraSpeed; } set { cameraSpeed = value; } }
    [SerializeField] Transform cameraTransform;
    [SerializeField] float speedModifier = 1;
    public float SpeedModifier { get { return speedModifier; } set { speedModifier = value; } }
    [SerializeField] float deathYPos = -18;

    [SerializeField] GameObject gameOverPanel;
    private bool isGameOver = false;
    public bool IsGameOver { get { return isGameOver; } set { isGameOver = value; } }
    [SerializeField] GameObject inGameScore;

    private bool startGame = false;
    public bool StartGame { get { return startGame; } set { startGame = value; } }
    [SerializeField] GameObject startGameTextPrompt;

    [SerializeField] GameObject pausePanel;
    private bool isPaused = false;
    public bool IsPaused { get { return isPaused; } set { isPaused = value; } }

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
        startGameTextPrompt.SetActive(true);
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
                startGameTextPrompt.SetActive(false);
            }
        }

        Pause();
    }

    private void LateUpdate()
    {
        if (startGame)
        {
            // Move the camera in the x direction
            cameraTransform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0) * speedModifier;
        }       
    }  

    public void Restart(string _sceneName)
    {
        SceneManager.LoadScene(_sceneName);
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

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else if (!isPaused)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Resume()
    {
        isPaused = false;
    }
}
