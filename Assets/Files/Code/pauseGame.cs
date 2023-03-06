using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class pauseGame : MonoBehaviour
{
    [HideInInspector]
    public static bool paused = false;
    private GameObject quitButton;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
        quitButton = GameObject.FindGameObjectWithTag("QuitButton");
        quitButton.GetComponent<Button>().interactable = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) // Note: Added P, as most browsers react to Escape
        {
            if (paused == false)
            {
                paused = true;
                PauseGame();
                quitButton.GetComponent<Button>().interactable = true;
            }
            else
            {
                quitButton.GetComponent<Button>().interactable = false;
                paused = false;
                ResumeGame();
            }
        }
    }



    public void PauseGame()
    {
        Time.timeScale = 0;
        AudioListener.pause = true;
        gameObject.GetComponent<CanvasGroup>().alpha = 1;
    }
    public void ResumeGame()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }
}
