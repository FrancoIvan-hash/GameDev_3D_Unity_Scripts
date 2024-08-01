using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsLayerMask;

    private bool isBusy;

    private void Awake()
    {
        // checks that we only have one Instance
        if (Instance != null)
        {
            Debug.LogError("There's more that one UnitActionSystem! " + transform + " - " + Instance);
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    private void Update()
    {
        if (isBusy) { return; }

        // if using LMB
        if (Input.GetMouseButtonDown(0))
        {
            // try handling the unit then return if succeeded
            if (TryHandleUnitSelection()) { return; }

            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
            {
                SetBusy();
                // move the current selected unit
                selectedUnit.GetMoveAction().Move(mouseGridPosition, ClearBusy);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            SetBusy();
            selectedUnit.GetSpinAction().Spin(ClearBusy);
        }
    }

    private void SetBusy()
    {
        isBusy = true;
    }

    private void ClearBusy()
    {
        isBusy = false;
    }

    // select a unit when clicking 
    private bool TryHandleUnitSelection()
    {
        // do a ScreenPointToRay to get the mousePosition as a ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // do a Raycast to check whether we hit something of LayerMask = Units
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitsLayerMask))
        {
            // try to get the Unit component of whatever we hit
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                // set the selectedUnit to the unit we hit when casting a Raycast
                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        // Set selectedUnit
        selectedUnit = unit;

        // Fire the event
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    // exposes the selectedUnit, but makes sure that it doesn't get modified
    public Unit GetSelectedUnit()
    {
        // return our selectedUnit
        return selectedUnit;
    }
}
