using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    [SerializeField] private CharacterController controller;

    private float verticalVelocity;

    public Vector3 movement => Vector3.up * verticalVelocity;

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
    }
}
