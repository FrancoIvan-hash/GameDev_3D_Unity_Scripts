using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;
    private int health;

    // Start is called before the first frame update
    private void Start()
    {
        health = maxHealth;
    }

    public void DealDamage(int damage)
    {
        // make sure the character isn't dead already
        if (health == 0) { return; }

        health -= damage;

        // this sets the health to 0 if it ever gets to a negative value
        health = Mathf.Max(health - damage, 0);

        Debug.Log(health);
    }
}