using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[ExecuteAlways] // will execute in playmode and editmode
public class CoordinateLabeler : MonoBehaviour
{
    private TextMeshPro label;
    Vector2Int coordinates = new Vector2Int(); // 2D coords using ints

    private void Awake()
    {
        label = GetComponent<TextMeshPro>();
        DisplayCoordinates(); // to make sure we get our coords right in playmode
    }
    // Update is called once per frame
    void Update()
    {
        if (!Application.isPlaying)
        {
            DisplayCoordinates();
            UpdateObjectName();
        }
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
}
