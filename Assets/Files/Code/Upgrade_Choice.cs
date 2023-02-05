using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade_Choice : MonoBehaviour
{
    public Image abilityIcon;
    public static Sprite tempSprite;
    
    class upgradePanel 
    {
        string name;
        string description;
        int cost;
        Sprite icon;

        upgradePanel()
        {
            name = "Name";
            description = "This is a description";
            cost = 0;
            icon = tempSprite; //"UI_Skill_Icon_Dash";
        }

        upgradePanel(string myName, string myDescription, int myCost, Sprite myIcon)
        {
            name = myName;
            description = myDescription;
            cost = myCost;
            icon = myIcon;
        }

    }
    public void ChangeSprite()
    {
        abilityIcon.sprite = upgradePanel().icon;
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
