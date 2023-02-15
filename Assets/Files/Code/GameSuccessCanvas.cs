using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSuccessCanvas : MonoBehaviour
{
    [SerializeField] Text message;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void showUI()
    {
        gameObject.SetActive(true);
        Time.timeScale = 0;
    }
}
