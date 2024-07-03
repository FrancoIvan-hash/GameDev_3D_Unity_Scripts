using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocator : MonoBehaviour
{
    [SerializeField] private Transform weapon;
    [SerializeField] private ParticleSystem projectileParticles;
    [SerializeField] private float range = 15f;
    private Transform target;

    // Update is called once per frame
    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    private void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null; // haven't found it yet
        float maxDistance = Mathf.Infinity; // stores furthest distance of all enemies

        // this loop is fine for our small project
        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);

            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance; // update maxDistance
            }
        }

        target = closestTarget;
    }

    private void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);

        weapon.LookAt(target.position);

        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    private void Attack(bool IsActive)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = IsActive;
    }
}
