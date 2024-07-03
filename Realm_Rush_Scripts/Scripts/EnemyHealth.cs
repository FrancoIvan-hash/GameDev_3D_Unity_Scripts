using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;
    [SerializeField] private int currentHitPoints = 0;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
        Debug.Log(currentHitPoints);
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }

    private void ProcessHit()
    {
        currentHitPoints--; //decrement currentHitPoints by 1

        if (currentHitPoints <= 0)
        {
            // Destroy(this.gameObject);
            gameObject.SetActive(false); // hide gameObject   
        }
    }
}
