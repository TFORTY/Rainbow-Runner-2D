using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player"); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Border")
        {
            Destroy(gameObject);
        }
        else if (collision.tag == "Player")
        {
            if (GameManager.Instance.IsInvinciblePowerup)
            {
                // invincible
                // start timer
            }
            else
            {
                GameManager.Instance.GameOver();
            }          
        }
        
    }
}
