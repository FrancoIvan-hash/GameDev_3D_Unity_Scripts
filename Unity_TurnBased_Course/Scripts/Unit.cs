using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    // Get access to the Animator attached to the Unit/Player gameobject to change the animations
    [SerializeField] private Animator unitAnimator;
    // this is the position we want the Unit to move to
    private Vector3 targetPosition;

    private void Awake()
    {
        // initialize the targetPosition to the Unit's current position at the beginning
        targetPosition = transform.position;
    }

    private void Update()
    {
        float stoppingDistance = .1f; // a small offset to stop before reaching our target position
        if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
        {
            // we want direction vector, not magnitude included
            // use normalized for that 
            Vector3 moveDirection = (targetPosition - transform.position).normalized;
            float moveSpeed = 4f;
            transform.position += moveDirection * moveSpeed * Time.deltaTime;

            float rotateSpeed = 10f;
            // smoothly rotate towards moveDirection
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * rotateSpeed);

            // change animation to walking
            unitAnimator.SetBool("IsWalking", true);
        }
        else
        {
            // change animation to idle         
            unitAnimator.SetBool("IsWalking", false);
        }
    }

    // Using in UnitActionSystem
    public void Move(Vector3 targetPosition)
    {
        // set our target position to the param
        this.targetPosition = targetPosition;
    }
}
