using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade_Choice : MonoBehaviour
{
    public Image abilityIcon;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public Sprite[] spriteArray;
    public AbilityTracking abilityTracker;
    public AbilityHider abilityRevealer;

    public string[] nameArray = {"Beam", "Black Hole", "Roll", "Rock", "Heal"};
    public string[] descriptionArray = {"Description 1","Description 2","Description 3","Description 4","Description 5"};
    public int[] costArray = {5,10,5,5,10};

    public class upgradePanel 
    {
        string name;
        string description;
        int cost;
        Sprite icon;

        public upgradePanel(string myName, string myDescription, int myCost, Sprite myIcon)
        {
            name = myName;
            description = myDescription;
            cost = myCost;
            icon = myIcon;
        }

        public Sprite getIcon()
        {
            return icon;
        }

        public int getCost()
        {
            return cost;
        }

        public string getDescription()
        {
            return description;
        }

        public string getName()
        {
            return name;
        }
    }

    public void generatePanelChoice()
    {
        upgradePanel[] tempPanelArray = makePanelArray(); //makes an array of all the panels
        int r = randomNum(0, tempPanelArray.Length); //generates a random index of the panel array
        abilityIcon.sprite = tempPanelArray[r].getIcon(); //sets ability icon to current panel from the array
        nameText.text = tempPanelArray[r].getName() + "\n Cost: " + tempPanelArray[r].getCost();
        descriptionText.text = tempPanelArray[r].getDescription();
    }

    public void selectUpgrade()
    {
        generatePanelChoice();
        gameObject.SetActive(true); //reveals ability selection screen
        //finds way to determine what upgrade is selected
        abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Roll);
        abilityRevealer.Reveal();
    }

    public upgradePanel[] makePanelArray() //returns an array of all the upgrade panel data
    {
        upgradePanel[] tempPanelArray = new upgradePanel[spriteArray.Length]; //makes an upgrade panel array of size equal to the num sprites
        for (int i = 0; i < tempPanelArray.Length; i++)
        {
            upgradePanel tempPanel = new upgradePanel(nameArray[i], descriptionArray[i], costArray[i], spriteArray[i]);
            tempPanelArray[i] = tempPanel;
        }
        return tempPanelArray;
    }

    int randomNum(int low, int high) //makes random number from low to high
    {
        System.Random rd = new System.Random();
        int num = rd.Next(low, high);
        return num;
    }

    void Start()
    {
        gameObject.SetActive(false);
    }
}
