using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;

    private List<Target> targets = new List<Target>();

    public Target CurrentTarget { get; private set; }
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target isTarget)) { return; }

        targets.Add(isTarget);

        isTarget.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target isTarget)) { return; }

        RemoveTarget(isTarget); // removes the target 
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        Target closestTarget = null;
        float closestTargetDistance = Mathf.Infinity; // need this number to be big

        foreach (Target target in targets)
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            // check whether the target is on the screen
            if (viewPos.x < 0 || viewPos.x > 1 || viewPos.y < 0 || viewPos.y > 1) { continue; }

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f); // (0.5f, 0.5f) is the center of our screen
            if (toCenter.sqrMagnitude < closestTargetDistance)
            {
                closestTarget = target;
                closestTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closestTarget == null) { return false; }

        CurrentTarget = closestTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f); // add target 

        return true;
    }

    public void Cancel()
    {
        if (CurrentTarget == null) { return; }

        cineTargetGroup.RemoveMember(CurrentTarget.transform); // remove target
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        // this is when a target is destroyed before trigerring the OnTriggerExit method
        if (CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
