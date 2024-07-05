using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int maxHitPoints = 5;

    [Tooltip("Adds amount to maxHitPoints when enemy dies")]
    [SerializeField] private int difficultyRamp = 1;

    private int currentHitPoints = 0;

    private Enemy enemy;

    // Start is called before the first frame update
    void OnEnable()
    {
        currentHitPoints = maxHitPoints;
    }

    private void Start()
    {
        enemy = GetComponent<Enemy>();
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
            maxHitPoints += difficultyRamp;
            enemy.RewardGold();
        }
    }
}
