using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class quitButton : MonoBehaviour
{
    void start()
    {
        quitGame();
    }
    public void quitGame()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
    }
}
