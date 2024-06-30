using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem; for new input system

public class PlayerController : MonoBehaviour
{
    // [SerializeField] InputAction movement;
    [Header("General Setup Settings")]
    [Tooltip("How fast ship moves up/down")]
    [SerializeField] private float controlSpeed = 20f;
    [Tooltip("How far player moves horizontally")]
    [SerializeField] private float xRange = 7f;
    [Tooltip("How far player moves vertically")]
    [SerializeField] private float yRange = 5f;

    [Header("Laser gun array")]
    [Tooltip("Add all player lasers")][SerializeField] private GameObject[] lasers;

    [Header("Screen position based tuning")]
    [SerializeField] private float positionPitchFactor = -5f;
    [SerializeField] private float positionYawFactor = 7f;

    [Header("Player input based tuning")]
    [SerializeField] private float controlPitchFactor = -14f;
    [SerializeField] private float controlRollFactor = -28f;


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
        ProcessFiring();
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

    private void ProcessFiring()
    {
        // if pushing fire button
        // then print "shooting"
        // else don't print "shooting"
        if (Input.GetButton("Fire1"))
        {
            SetLasersActive(true);
        }
        else
        {
            SetLasersActive(false);
        }
    }

    private void SetLasersActive(bool isActive)
    {
        // for each of the lasers we have, turn them on
        foreach (GameObject laser in lasers)
        {
            // laser.SetActive(true);
            ParticleSystem ps = laser.GetComponent<ParticleSystem>();
            var em = ps.emission;
            em.enabled = isActive;
        }
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