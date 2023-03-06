using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameRestart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void restart()
    {
        GameObject[] loaders = GameObject.FindGameObjectsWithTag("DynamicLoader");
        foreach (GameObject loader in loaders) {
            loader.GetComponent<DynamicLoader>().unloadAssets();
         }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("a");
    }
}
