using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement
{
    public class Mover : MonoBehaviour, IAction
    {
        private NavMeshAgent navMeshAgent;
        private Health health;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            health = GetComponent<Health>();
        }

        // Update is called once per frame
        private void Update()
        {
            navMeshAgent.enabled = !health.IsDead();

            UpdateAnimator();
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(destination);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        private void UpdateAnimator()
        {
            // get global velocity (takes into account where the player is in the world)
            Vector3 velocity = GetComponent<NavMeshAgent>().velocity;
            // convert this value into local velocity (doesn't matter where player is in the world)
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            // animation adds a velocity on the z axis
            float speed = localVelocity.z;
            // change animation
            GetComponent<Animator>().SetFloat("forwardSpeed", speed);
        }
    }
}
