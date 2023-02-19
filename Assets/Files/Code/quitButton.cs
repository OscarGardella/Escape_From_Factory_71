using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quitButton : MonoBehaviour
{
    public OpeningAnimHackText hackText;
    void start()
    {
        GameObject hackObj = GameObject.FindGameObjectWithTag("Hack");
        if(! hackObj) Debug.Log("quitButton.cs: Error: Failed to find game object by tag \"Hack\" This script will probably crash.");
        hackText = hackObj.GetComponent<OpeningAnimHackText>();
        quitGame();
    }
    public void quitGame()
    {
        if(hackText)
            hackText.resetAnim();
        
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
