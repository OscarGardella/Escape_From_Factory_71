using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSuccessCanvas : MonoBehaviour
{
    [SerializeField] Text message;
    [SerializeField] private CanvasGroup myUIGroup;
    [SerializeField] private bool fadeIn = false;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject.SetActive(false);
    }

    public void showUI()
    {
        fadeIn = true;
        //gameObject.SetActive(true);
    }
    
    private void Update()
    {
        if (fadeIn)
        {
            if(myUIGroup.alpha < 1)
            {
                myUIGroup.alpha += Time.deltaTime;
                if(myUIGroup.alpha >= 1)
                {
                    fadeIn = false;
                }
            }
        }
    }
}
