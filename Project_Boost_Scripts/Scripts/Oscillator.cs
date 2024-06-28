using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Mathf.Epsilon is the smallest decimal number in Unity
        if (period <= Mathf.Epsilon) return; // make sure we don't divide cycles by 0

        float cycles = Time.time / period; // continually growing over time
        const float tau = 2 * Mathf.PI; // constant value 
        float rawSinWave = Mathf.Sin(cycles * tau); // get a sine wave (going from -1 to 1)

        movementFactor = (rawSinWave + 1f) / 2f; // to go from 0 to 1 (not -1 to 1)

        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition + offset;
    }
}
