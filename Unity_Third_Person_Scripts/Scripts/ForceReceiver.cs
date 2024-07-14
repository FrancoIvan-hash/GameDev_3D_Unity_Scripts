using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AI;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float drag = 0.3f;

    private float verticalVelocity;

    private Vector3 dampingVelocity; // reference used in SmoothDamp 

    private Vector3 impact; // knockback or other external forces

    public Vector3 Movement => impact + Vector3.up * verticalVelocity;

    private void Update()
    {
        if (verticalVelocity < 0 && controller.isGrounded)
        {
            // we're setting the verticalVelocity to a small value when we're on the ground
            verticalVelocity = Physics.gravity.y * Time.deltaTime;
        }
        else
        {
            // we're incrementing the verticalVelocity when we're not on the ground
            verticalVelocity += Physics.gravity.y * Time.deltaTime;
        }

        impact = Vector3.SmoothDamp(impact, Vector3.zero, ref dampingVelocity, drag);

        // turning on navmeshagent component when there's no forces acting on the enemy
        if (agent != null)
        {
            if (impact.sqrMagnitude <= 0.2f * 0.2f)
            {
                impact = Vector3.zero;
                agent.enabled = true;
            }
        }
    }

    public void AddForce(Vector3 force)
    {
        impact += force;

        // if we're the enemy, turn off navmeshagent component so that 
        // it doesn't fight the knockback force we're adding
        if (agent != null)
        {
            agent.enabled = false;
        }
    }
}
