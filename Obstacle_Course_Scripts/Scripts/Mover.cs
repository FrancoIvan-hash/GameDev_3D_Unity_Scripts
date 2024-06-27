using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 8.0f; // found from previous project

    // Start is called before the first frame update
    void Start()
    {
        //PrintInstructions();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer(); // move player
    }

    void PrintInstructions() 
    {
        Debug.Log("Welcome to the game");
        Debug.Log("You will need to avoid things!");
        Debug.Log("Move your player with WASD or arrow keys");
    }

    void MovePlayer()
    {
        // get movement input for player
        float xValue = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        float zValue = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;

        transform.Translate(xValue, 0.0f, zValue);
    }
}
