using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int playerHealth = 100;
    [SerializeField] Text healthText;

    private void Start()
    {
        healthText.text = playerHealth.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        playerHealth = playerHealth - 20;
        healthText.text = playerHealth.ToString();
        if (playerHealth <= 0)
        {
            GameOver();
        }
    }
void GameOver()
    {
        print("You lose");
    }
}
