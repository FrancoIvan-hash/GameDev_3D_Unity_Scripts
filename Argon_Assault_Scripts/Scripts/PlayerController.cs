using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem; for new input system

public class PlayerController : MonoBehaviour
{
    // [SerializeField] InputAction movement;
    [SerializeField] private float controlSpeed = 15f;
    [SerializeField] private float xRange = 2f;
    [SerializeField] private float yRange = 3f;
    [SerializeField] private float positionPitchFactor = -8f;
    [SerializeField] private float controlPitchFactor = -20f;
    [SerializeField] private float positionYawFactor = 10f;
    [SerializeField] private float controlRollFactor = -40f;


    private float xThrow;
    private float yThrow;

    // Update is called once per frame
    void Update()
    {
        // New Input System
        // float horizontalThrow = movement.ReadValue<Vector2>().x;
        // float verticalThrow = movement.ReadValue<Vector2>().y;

        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessTranslation()
    {
        // get value from keyboard input
        xThrow = Input.GetAxis("Horizontal");
        yThrow = Input.GetAxis("Vertical");

        // changing x position on Player Ship (using localPosition)
        float xOffSet = xThrow * Time.deltaTime * controlSpeed;
        float rawXPos = transform.localPosition.x + xOffSet;
        float clampedXPos = Mathf.Clamp(rawXPos, -xRange, xRange);

        // changing y position on Player Ship
        float yOffSet = yThrow * Time.deltaTime * controlSpeed;
        float rawYPos = transform.localPosition.y + yOffSet;
        float clampedYPos = Mathf.Clamp(rawYPos, -yRange, yRange);

        transform.localPosition = new Vector3(clampedXPos, clampedYPos, transform.localPosition.z);
    }

    private void ProcessRotation()
    {
        float pitchDueToPosition = transform.localPosition.y * positionPitchFactor;
        float pitchDueToControlThrow = yThrow * controlPitchFactor;

        float pitch = pitchDueToPosition + pitchDueToControlThrow;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = xThrow * controlRollFactor;
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }
}
/*
  // have to enable new input system when game starts
  private void OnEnable()
  {
      movement.Enable();
  }

  // have to disable new input system when game closes
  private void OnDisable()
  {
      movement.Disable();
  }
  */