using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    // This is a Singleton script kind of
    public static MouseWorld instance;

    // organizing our plane in mousePlaneLayerMask so that we move the Unit in the correct space
    [SerializeField] private LayerMask mousePlaneLayerMask;

    private void Awake()
    {
        instance = this;
    }

    // get the position of our mouse
    public static Vector3 GetPosition()
    {
        // get mousePosition as a Ray
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        // do a Raycast to check if we hit something
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        // return point=where we hit something in mousePlaneLayerMask
        return raycastHit.point;
    }
}
