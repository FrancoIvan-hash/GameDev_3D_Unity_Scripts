using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float mainThrust = 1000.0f;
    [SerializeField] float rotationThrust = 200.0f; // change z rotation value
    [SerializeField] AudioClip mainEngine;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem leftThrusterParticles1;
    [SerializeField] ParticleSystem leftThrusterParticles2;
    [SerializeField] ParticleSystem rightThrusterParticles1;
    [SerializeField] ParticleSystem rightThrusterParticles2;

    // CACHE - e.g. references for readability or speed
    Rigidbody rb;
    //RigidbodyInterpolation rbInterpolate;
    AudioSource rocketSound;

    // STATE - private instance (member) variables

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); // get reference for Rigidbody
        rocketSound = GetComponent<AudioSource>(); // get reference to AudioSource
        //rbInterpolate = GetComponent<RigidbodyInterpolation>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust(); // calling method for thrusting rocket (relative Y movement)
        ProcessRotation(); // calling method for rotating rocket (over z rotation)
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting(); // used Extract Method (CTRL .)
        }
        else
        {
            StopThrusting(); // used Extract Method (CTRL .)
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft(); // used Extract Method (CTRL .)
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight(); // used Extract Method (CTRL .)
        }
        else
        {
            StopRotating(); // used Extract Method (CTRL .)
        }
    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!rocketSound.isPlaying) // make sure sound isn't currently playing
            rocketSound.PlayOneShot(mainEngine); // play AudioSource

        if (!mainEngineParticles.isPlaying) // make sure particles system isn't playing 
            mainEngineParticles.Play(); // play the main engine particle system
    }

    private void StopThrusting()
    {
        rocketSound.Stop(); // don't play if we're not thrusting
        mainEngineParticles.Stop(); // stop particle system if not thrusting
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust); // rotate left (0, 0, 1);

        if (!rightThrusterParticles1.isPlaying && !rightThrusterParticles2.isPlaying)
        { // make sure right thruster particles isn't playing
            rightThrusterParticles1.Play(); // play particle system (opposite of rotation)
            rightThrusterParticles2.Play(); // play particle system (opposite of rotation)
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust); // rotate right (0, 0, -1)

        if (!leftThrusterParticles1.isPlaying && !leftThrusterParticles2.isPlaying)
        { // make sure left thruster particle isn't playing
            leftThrusterParticles1.Play(); // play particle system
            leftThrusterParticles2.Play(); // play particle system
        }
    }

    private void StopRotating()
    {
        rightThrusterParticles1.Stop(); // stop particle system if not rotating left
        rightThrusterParticles2.Stop();
        leftThrusterParticles1.Stop(); // stop particle system if not rotating right
        leftThrusterParticles2.Stop();
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // add constraints so physics system doesn't cause a bug
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // unfreeze rotation so the physics system can take over
    }
}
