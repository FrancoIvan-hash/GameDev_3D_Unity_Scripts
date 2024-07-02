using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathFX;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private int increaseScoreByHit;
    [SerializeField] private int hitPoints = 2; // how much hits an enemy can take before exploding

    private ScoreBoard scoreBoard;
    private GameObject parentGameObject;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");
        AddRigidbody();
    }

    private void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints < 1) { KillEnemy(); }
    }

    private void ProcessHit()
    {
        PlayParticleSystem(hitVFX);
        hitPoints--; // decrement the hitPoints ever particle collision
    }

    private void KillEnemy()
    {
        // increase score
        scoreBoard.IncreaseScore(increaseScoreByHit);
        PlayParticleSystem(deathFX);
        Destroy(this.gameObject);
    }

    private void PlayParticleSystem(GameObject particleSystem)
    {
        // Quaternion.identity is a way of saying we don't want any rotation
        GameObject fx = Instantiate(particleSystem, this.transform.position, Quaternion.identity);
        // take all instances and organized them under parent GameObject/Transform (it's an empty gameObject)
        fx.transform.parent = parentGameObject.transform;
    }
}
