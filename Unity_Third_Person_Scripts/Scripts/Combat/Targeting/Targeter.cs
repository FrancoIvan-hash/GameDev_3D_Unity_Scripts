using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    public List<Target> targets = new List<Target>();

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
}
