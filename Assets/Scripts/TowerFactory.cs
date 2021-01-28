using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit = 5;
    [SerializeField] Tower tower;

    Queue<Tower> towers = new Queue<Tower>();

    private void Start()
    {
        
    }
    public void AddTower(Waypoint waypoint)
    {
        int currentTowers = towers.Count;
        if (currentTowers < towerLimit)
        {
            InstantiateNewTower(waypoint);
        }
        else
        {
            MoveExistingTower(waypoint);
        }
    }

    private void InstantiateNewTower(Waypoint waypoint)
    {
        var newTower = Instantiate(tower, waypoint.transform.position, Quaternion.identity);
        waypoint.isPlaceable = false;
        newTower.baseWaypoint = waypoint;
        newTower.baseWaypoint.isPlaceable = false;
        newTower.transform.parent = transform.GetChild(1);
        towers.Enqueue(newTower);
    }

    private void MoveExistingTower(Waypoint waypoint)
    {
        var tower = towers.Dequeue();
        tower.baseWaypoint.isPlaceable = true;
        waypoint.isPlaceable = false;
        tower.baseWaypoint = waypoint;
        tower.transform.position = waypoint.transform.position;
        towers.Enqueue(tower);
    }
}
