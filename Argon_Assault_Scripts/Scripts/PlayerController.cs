using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // [SerializeField] InputAction movement;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    /*
    private void OnEnable()
    {
        movement.Enable();
    }

    rivate void OnDisable()
    {
        movement.Disable();
    }
    */

    // Update is called once per frame
    void Update()
    {
        // float horizontalThrow = movement.ReadValue<Vector3>().x;
        float horizontalThrow = Input.GetAxis("Horizontal");
        Debug.Log(horizontalThrow);

        // float verticalThrow = movement.ReadValue<Vector2>().y;
        float verticalThrow = Input.GetAxis("Vertical");
        Debug.Log(verticalThrow);
    }
}
