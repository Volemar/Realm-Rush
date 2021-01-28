using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] EnemyMovement enemy;
    [SerializeField] float secondsToSpawn = 5f;
    [SerializeField] int enemiesCount = 5;
    [SerializeField] Transform parentTransform;
    [SerializeField] Text scoreText;
    [SerializeField] int score = 0;

    private void Start()
    {
        if (scoreText.text == "")
        {
            scoreText.text = score.ToString();
        }
        StartCoroutine(SpawnEnemy(enemiesCount));
    }

    IEnumerator SpawnEnemy(int number)
    {
        for (int i = 0; i < number; i++)
        {
            EnemyMovement newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);
            newEnemy.transform.parent = parentTransform;
            score = int.Parse(scoreText.text);
            score++;
            scoreText.text = score.ToString();
            yield return new WaitForSeconds(secondsToSpawn);
        }
    }
}
