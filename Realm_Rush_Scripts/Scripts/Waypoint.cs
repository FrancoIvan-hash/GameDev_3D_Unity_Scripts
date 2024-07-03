using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } // this is a property of isPlaceable

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            Instantiate(towerPrefab, transform.position, Quaternion.identity);
            isPlaceable = false; // make sure you can't place a tower on top of a tower
        }
    }
}
