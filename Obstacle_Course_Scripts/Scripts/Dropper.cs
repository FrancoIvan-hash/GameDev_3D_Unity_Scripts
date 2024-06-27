using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    MeshRenderer renderer;
    Rigidbody rigidBody;
    [SerializeField] float timeToWait = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        rigidBody = GetComponent<Rigidbody>();

        renderer.enabled = false; // make GameObject invisible
        rigidBody.useGravity = false; // no gravity to GameObject
    }

    // Update is called once per frame
    void Update()
    {
        // Time.time --> timer
        if (Time.time > timeToWait)
        {
            //Debug.Log(timeToWait + " seconds have elapsed!");
            renderer.enabled = true;
            rigidBody.useGravity = true;
        }
        //Debug.Log("This many seconds have passed: " + Time.time);
    }
}
