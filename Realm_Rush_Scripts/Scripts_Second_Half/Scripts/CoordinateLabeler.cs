using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] // will execute in playmode and editmode
[RequireComponent(typeof(TextMeshPro))]
public class CoordinateLabeler : MonoBehaviour
{
    [SerializeField] private Color defaultColor = Color.black;
    [SerializeField] private Color blockedColor = Color.gray;
    [SerializeField] private Color exploredColor = Color.yellow;
    [SerializeField] private Color pathColor = new Color(1f, 0.5f, 0f); // this is orange

    private TextMeshPro label;
    private Vector2Int coordinates = new Vector2Int(); // 2D coords using ints
    private GridManager gridManager;

    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        DisplayCoordinates(); // to make sure we get our coords right in playmode
    }
    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
            label.enabled = true; // enable label in editmode
        }

        // these scripts get called in playmode
        SetLabelColor();
        ToggleLabels();
    }

    private void DisplayCoordinates()
    {
        if (gridManager == null) { return; }
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / gridManager.UnityGridSize);
        // we're working on x, z coord (not y)
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / gridManager.UnityGridSize);

        label.text = coordinates.x + "," + coordinates.y;
    }

    private void UpdateObjectName()
    {
        // we're naming our tiles corresponding to their coords
        transform.parent.name = coordinates.ToString();
    }

    private void SetLabelColor()
    {
        if (gridManager == null) { return; }

        Node node = gridManager.GetNode(coordinates);

        if (node == null) { return; }

        if (!node.isWalkable)
        {
            // gray
            label.color = blockedColor;
        }
        else if (node.isPath)
        {
            // orange
            label.color = pathColor;
        }
        else if (node.isExplored)
        {
            // yellow
            label.color = exploredColor;
        }
        else
        {
            // black
            label.color = defaultColor;
        }
    }

    private void ToggleLabels()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            label.enabled = !label.IsActive();
        }
    }
}
