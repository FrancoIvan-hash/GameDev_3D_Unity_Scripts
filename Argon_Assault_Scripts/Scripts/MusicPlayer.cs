using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is a Singleton Class
public class MusicPlayer : MonoBehaviour
{
    private void Awake()
    {
        int numMusicPlayer = FindObjectsOfType<MusicPlayer>().Length;

        // make sure we only have one instance
        if (numMusicPlayer > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
