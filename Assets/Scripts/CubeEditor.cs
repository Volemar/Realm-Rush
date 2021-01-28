using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
[RequireComponent(typeof(Waypoint))]
public class CubeEditor : MonoBehaviour
{
    Waypoint waypoint;
    private void Awake()
    {
        waypoint = GetComponent<Waypoint>();
    }
    void Update()
    {
        SnapToGrid();
        UpdateLabel();
    }

    private void SnapToGrid()
    {
        int gridSize = waypoint.GetGridSize();
        transform.position = new Vector3(
            waypoint.GetGridPos().x * gridSize, 
            0f,
            waypoint.GetGridPos().y * gridSize
            );
    }

    private void UpdateLabel()
    {
        string textLabel = 
            waypoint.GetGridPos().x
            + "," + 
            waypoint.GetGridPos().y;
        TextMesh text = GetComponentInChildren<TextMesh>();
        text.text = textLabel;
        gameObject.name = "Cube (" + textLabel + ")";
    }
}
