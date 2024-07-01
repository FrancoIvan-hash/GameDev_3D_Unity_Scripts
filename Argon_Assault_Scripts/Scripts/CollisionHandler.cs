using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1.0f;
    [SerializeField] private ParticleSystem crashVFX;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{this.name} triggered with {other.gameObject.name}");
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        crashVFX.Play();
        GetComponent<MeshRenderer>().enabled = false; // to turn off meshrenderer but it doesn't work for my ship
        GetComponent<BoxCollider>().enabled = false; // so that we don't collide anymore
        GetComponent<PlayerController>().enabled = false;
        Invoke(nameof(ReloadLevel), loadDelay);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
