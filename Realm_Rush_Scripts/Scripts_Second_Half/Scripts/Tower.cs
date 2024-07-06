using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private int cost = 75;
    [SerializeField] private float buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(Build());
    }

    // instantiates a tower on the gameworld and returns true if it's successful, false otherwise
    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();

        if (bank == null) { return false; }

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower.gameObject, position, Quaternion.identity);
            bank.Withdraw(cost);
            return true;
        }

        // if everything else fails, return false by default
        return false;
    }

    private IEnumerator Build()
    {
        // turns off/disables all ballistas 
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
            // access the child of the child of ballista (trying to get the ParticleSystem)
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        // turns on/enables all ballistas
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(true);
            yield return new WaitForSeconds(buildDelay);
            // access the child of the child of ballista (trying to get the ParticleSystem)
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
        }
    }
}
