using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    // Detect collisions
    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log("Bumped onto a wall");
        if (other.gameObject.tag == "Player")
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
            gameObject.tag = "Hit"; // accessing this gameObject (not other.gameObject)
        }
    }
}
