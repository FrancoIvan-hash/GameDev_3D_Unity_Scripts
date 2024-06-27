using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitApplication : MonoBehaviour
{
    // Update is called once per frame
   
    public void QuitGame() 
    {
        Debug.Log("You have quit the game :(");
        Application.Quit(); // quits the game
    }
}
