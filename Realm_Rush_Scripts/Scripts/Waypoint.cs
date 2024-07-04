using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Tower towerPrefab;
    [SerializeField] private bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } // this is a property of isPlaceable

    private void OnMouseDown()
    {
        if (isPlaceable)
        {
            bool isPlaced = towerPrefab.CreateTower(towerPrefab, transform.position); // returns true if tower placed
            isPlaceable = !isPlaced; // make sure you can't place a tower on top of a tower
        }
    }
}
