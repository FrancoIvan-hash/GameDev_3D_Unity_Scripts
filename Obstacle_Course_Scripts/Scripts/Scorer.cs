using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    int hits = 0; // initial value of hits 

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Hit")
        {
            hits++; // increment number of hits when collision happens
            Debug.Log("Hits = " + hits);
        }
        // else don't add hit
    }
}
