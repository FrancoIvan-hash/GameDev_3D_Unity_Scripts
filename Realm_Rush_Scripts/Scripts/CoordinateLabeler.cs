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

    private TextMeshPro label;
    Vector2Int coordinates = new Vector2Int(); // 2D coords using ints
    Waypoint waypoint;

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        label.enabled = false;

        waypoint = GetComponentInParent<Waypoint>(); // bc this script is attached to child
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
        coordinates.x = Mathf.RoundToInt(transform.parent.position.x / UnityEditor.EditorSnapSettings.move.x);
        // we're working on x, z coord (not y)
        coordinates.y = Mathf.RoundToInt(transform.parent.position.z / UnityEditor.EditorSnapSettings.move.z);

        label.text = coordinates.x + "," + coordinates.y;
    }

    private void UpdateObjectName()
    {
        // we're naming our tiles corresponding to their coords
        transform.parent.name = coordinates.ToString();
    }

    private void SetLabelColor()
    {
        if (waypoint.IsPlaceable)
        {
            label.color = defaultColor;
        }
        else
        {
            label.color = blockedColor;
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
