using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{ 
    public void startScene()
    {
        SceneManager.LoadScene("Sprint_One_Demo_Scene");
    }
}
