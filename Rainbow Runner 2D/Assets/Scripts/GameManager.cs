using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField] Transform playerTransform;
    [SerializeField] Transform cameraTransform;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        playerTransform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
    }

    private void LateUpdate()
    {
        cameraTransform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0);
    }
}
