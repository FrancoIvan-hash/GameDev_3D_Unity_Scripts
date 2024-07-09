using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    private List<Target> targets = new List<Target>();

    public Target CurrentTarget { get; private set; }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target isTarget)) { return; }

        targets.Add(isTarget);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target isTarget)) { return; }

        targets.Remove(isTarget);
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; }

        CurrentTarget = targets[0];

        return true;
    }

    public void Cancel()
    {
        CurrentTarget = null;
    }
}