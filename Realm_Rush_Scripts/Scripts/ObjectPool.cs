using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private int poolSize = 5;
    [SerializeField] private float spawnTimer = 1f;

    private GameObject[] pool;

    private void Awake()
    {
        PopulatePool();
    }

    private void PopulatePool()
    {
        pool = new GameObject[poolSize];

        for (int i = 0; i < pool.Length; i++)
        {
            pool[i] = Instantiate(enemyPrefab, transform);
            pool[i].SetActive(false);
        }
    }

    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    private void EnableObjectInPool()
    {
        for (int i = 0; i < pool.Length; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].SetActive(true);
                return;
            }
        }
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            // Instantiate(object to instantiate, Transform parent)
            // Instantiate(enemyPrefab, transform);
            EnableObjectInPool();
            yield return new WaitForSeconds(spawnTimer);
        }
    }
}
