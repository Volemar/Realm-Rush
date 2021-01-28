using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
    [SerializeField]Waypoint start, end;
    Queue<Waypoint> queue = new Queue<Waypoint>();
    bool isRunning = true;
    Waypoint searchCenter;
    List<Waypoint> path = new List<Waypoint>();

    Vector2Int[] directions =
    {
        Vector2Int.up,
        Vector2Int.down,
        Vector2Int.left,
        Vector2Int.right
    };

    public List<Waypoint> GetPath()
    {
        if (path.Count > 0)
        {
            return path;
        }
        LoadBlocks();
        ColorStartEnd();
        BreadthFirstSearch();
        CreatePath();
        return path;
    }

    void CreatePath()
    {
        path.Add(end);
        end.isPlaceable = false;

        Waypoint previous = end.exploredFrom;
        while (previous != start)
        {
            path.Add(previous);
            previous.isPlaceable = false;
            previous = previous.exploredFrom;
        }
        path.Add(start);
        start.isPlaceable = false;
        path.Reverse();
    }

    void BreadthFirstSearch()
    {
        queue.Enqueue(start);

        while (queue.Count > 0 && isRunning)
        {
            searchCenter = queue.Dequeue();
            searchCenter.isExplored = true;
            HaltIfEndFound();
            ExploreNeighbours();
        }
        print("Finished pathfinding?");
    }

    private void HaltIfEndFound()
    {
        if (searchCenter == end)
        {
            print("Stopped");
            isRunning = false;
        }
    }

    void ExploreNeighbours()
    {
        if (!isRunning)
        {
            return;
        }
        foreach (Vector2Int direction in directions)
        {
            Vector2Int neighbourCoordinates = searchCenter.GetGridPos() + direction;
            if(grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int explorationCoord)
    {
        Waypoint neighbour = grid[explorationCoord];
        if (!neighbour.isExplored || queue.Contains(neighbour))
        {
            queue.Enqueue(neighbour);
            neighbour.exploredFrom = searchCenter;
        }
    }

    void LoadBlocks()
    {
        var waypoints = FindObjectsOfType<Waypoint>();
        foreach (Waypoint waypoint in waypoints)
        {
            bool isOverlapping = grid.ContainsKey(waypoint.GetGridPos());
            if (isOverlapping)
            {
                Debug.LogWarning("Overlapping blocks" + waypoint);
            }
            else
            {
                grid.Add(waypoint.GetGridPos(), waypoint);
            }
        }
    }
    void ColorStartEnd()
    {
        start.SetTopColor(Color.red);
        end.SetTopColor(Color.black);
    }
}
