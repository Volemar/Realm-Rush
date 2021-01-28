using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Tower : MonoBehaviour
{
    [SerializeField] float visionDistance = 10f;
    [SerializeField] Transform objectToPan;
    [SerializeField] GameObject gun;

    Transform targetEnemy;
    public Waypoint baseWaypoint;
    void Update()
    {
        SetTargetEnemy();
        var bulletEmission = gun.GetComponent<ParticleSystem>().emission;
        if (targetEnemy)
        {
            if (Vector3.Distance(objectToPan.position, targetEnemy.position) <= visionDistance)
            {
                bulletEmission.enabled = true;
                objectToPan.LookAt(targetEnemy);
            }
            else
            {
                bulletEmission.enabled = false;
            }
        }
        else
        {
            bulletEmission.enabled = false;
        }
        
    }
    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<Enemy>();
        if (sceneEnemies.Length == 0) {return;}

        Transform closestEnemy = sceneEnemies[0].transform;

        foreach (Enemy enemy in sceneEnemies)
        {
            closestEnemy = GetClosestEnemy(closestEnemy, enemy.transform);
        }
        targetEnemy = closestEnemy;
    }

    private Transform GetClosestEnemy(Transform closestEnemy, Transform secondEnemy)
    {
        float first = Vector3.Distance(closestEnemy.position, transform.position);
        float second = Vector3.Distance(secondEnemy.position, transform.position);
        if (first <= second)
        {
            return closestEnemy;
        }
        return secondEnemy;
    }
}
