using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pauseGame : MonoBehaviour
{
    [HideInInspector]
    public static bool paused = false;
    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
        gameObject.GetComponent<CanvasGroup>().alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused == false)
            {
                paused = true;
                PauseGame();
            }
            else
            {
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
