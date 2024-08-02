using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystem : MonoBehaviour
{
    public static UnitActionSystem Instance { get; private set; }

    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsLayerMask;

    public event EventHandler OnSelectedUnitChanged;
    public event EventHandler OnSelectedActionChanged;
    public event EventHandler<bool> OnBusyChanged;
    public event EventHandler OnActionStarted;

    private BaseAction selectedAction;
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

    private void Start()
    {
        SetSelectedUnit(selectedUnit);
    }

    private void Update()
    {
        if (isBusy) { return; }

        if (EventSystem.current.IsPointerOverGameObject()) { return; }

        if (TryHandleUnitSelection()) { return; }

        HandleSelectedAction();
    }

    private void HandleSelectedAction()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

            if (!selectedAction.IsValidActionGridPosition(mouseGridPosition)) { return; }

            if (!selectedUnit.TrySpendActionPointsToTakeAction(selectedAction)) { return; }

            SetBusy();
            selectedAction.TakeAction(mouseGridPosition, ClearBusy);

            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }
    }

    private void SetBusy()
    {
        isBusy = true;

        OnBusyChanged?.Invoke(this, isBusy);
    }

    private void ClearBusy()
    {
        isBusy = false;

        OnBusyChanged?.Invoke(this, isBusy);
    }

    // select a unit when clicking 
    private bool TryHandleUnitSelection()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // do a ScreenPointToRay to get the mousePosition as a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // do a Raycast to check whether we hit something of LayerMask = Units
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitsLayerMask))
            {
                // try to get the Unit component of whatever we hit
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    if (unit == selectedUnit)
                    {
                        // this unit is already selected
                        return false;
                    }
                    // set the selectedUnit to the unit we hit when casting a Raycast
                    SetSelectedUnit(unit);
                    return true;
                }
            }
        }

        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        // Set selectedUnit
        selectedUnit = unit;

        SetSelectedAction(selectedUnit.GetMoveAction());

        // Fire the event
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }

    public void SetSelectedAction(BaseAction baseAction)
    {
        selectedAction = baseAction;

        OnSelectedActionChanged?.Invoke(this, EventArgs.Empty);
    }

    // exposes the selectedUnit, but makes sure that it doesn't get modified
    public Unit GetSelectedUnit() => selectedUnit;

    public BaseAction GetSelectedAction() => selectedAction;
}
