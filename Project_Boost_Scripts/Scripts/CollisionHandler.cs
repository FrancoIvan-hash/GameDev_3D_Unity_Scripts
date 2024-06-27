using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    // PARAMETERS - for tuning, typically set in the editor
    [SerializeField] float levelLoadDelay = 1.0f;
    [SerializeField] AudioClip crashClip; // sound for when rocket crashes
    [SerializeField] AudioClip successClip; // sound for when rocket makes it to the finish goal
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;

    // CACHE - e.g. references for readability or speed
    AudioSource audioSource;
    Rigidbody rb;

    // STATE - private instance (member) variables
    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // get/cache AudioSource
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //RespondToDebugKeys();
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
            LoadNextLevel();

        else if (Input.GetKeyDown(KeyCode.C))
            collisionDisable = !collisionDisable; // toggle collision
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable) // makes sure other sounds aren't played simultaneously
            return; // return from OnCollisionEnter

        switch (other.gameObject.tag)
        {
            case "Friendly":
                //Debug.Log("This thing is friendly");
                break;
            case "Finish":
                //Debug.Log("Congrats, bro/sis, you finished!");
                StartSuccessSequence();
                break;
            default:
                //Debug.Log("You died!");
                StartCrashSequence();
                //Invoke("ReloadLevel", 0.5f);
                break;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // current level
        SceneManager.LoadScene(currentSceneIndex); // reload current scene
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // get current scene index
        int nextSceneIndex = currentSceneIndex + 1; // to get next level
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) // if current level is last level
        {
            nextSceneIndex = 0; // set next level to first level (to continue playing) - this is a loop
        }
        SceneManager.LoadScene(nextSceneIndex); // load next scene/level
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop(); // stops all Audio
        audioSource.PlayOneShot(crashClip); // play crashing sound
        // to-do add particle effect upon crash
        crashParticles.Play(); // play explosion particles
        Movement movement = GetComponent<Movement>();// get Movement script on Player
        movement.enabled = false; // freeze rocket movement
        //rb.isKinematic = true;
        Invoke("ReloadLevel", levelLoadDelay); // reload current level after some delay
        //ReloadLevel(); // reload the level after crashing
    }

    void DestroyRocket()
    {
        Destroy(this.gameObject);
    }

    void StartSuccessSequence()
    {
        isTransitioning = true;
        // add audio 
        audioSource.Stop(); // stops all Audio
        audioSource.PlayOneShot(successClip); // play success sound
        // to-do add particle effect upon success
        successParticles.Play(); // play success particles
        Movement movement = GetComponent<Movement>(); // get Movement script on Player
        movement.enabled = false; // freeze rocket movement
        Invoke("LoadNextLevel", levelLoadDelay); // load next level after some delay
    }
}
