using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Upgrade_Choice : MonoBehaviour
{
    public Image oldSprite;
    public Image newSprite;
    
    public void ChangeSprite()
    {
        oldSprite.sprite = newSprite; 
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
