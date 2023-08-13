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
    [SerializeField] Transform cameraTransform;
    [SerializeField] float speedModifier = 1;

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

    // TODO
    // GAME OVER LOGIC
    // --> Trigger on bottom or when they collide with obstacle

    // Update is called once per frame
    void Update()
    {
             
    }

    private void LateUpdate()
    {
        // Move the camera in the x direction
        cameraTransform.position += new Vector3(cameraSpeed * Time.deltaTime, 0, 0) * speedModifier;
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
}
