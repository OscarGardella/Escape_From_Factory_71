using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }
    
    public void ShowFindExit()
    {
        gameObject.SetActive(true);
    }

    public void HideFindExit()
    {
        gameObject.SetActive(false);
    }
}
