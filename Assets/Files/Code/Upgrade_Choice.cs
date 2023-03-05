using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Upgrade_Choice : MonoBehaviour
{
    public Image abilityIcon;
    public TMP_Text nameText;
    public TMP_Text costText;
    public TMP_Text descriptionText;
    public AbilityTracking abilityTracker;
    public Upgrade_Choice otherUpgrade;
    
    public Sprite[] spriteArray;
    public string[] nameArray = {"Laser Shield", "Speed Roll", "Quick Target", "Nano Bot Armor", "Nuclear Battery", "Scuttler Legs"}; //"Ion Beam",
    public string[] desArray = {"Generate a glowing shield around you to fend off enemies lasers for a short period of time","Curl into a ball and roll at high speeds to evade enemies and travel faster","Gain powerful targeting software allowing you to fire lasers at twice the usual pace","Double your health and heal to full, allowing you to survive longer", "Install a new super powerful battery allowing you to regen power at an increased rate", "Install new legs on your robot allowing you to consistently move at a faster pace"}; //"Fire a large beam out of the front of your robot obliterating anything in your way"
    public int[] costArray = {8,6,4,0,0,0,0};

    public class upgradePanel
    {
        string name;
        string description;
        int cost;
        Sprite icon;

        public upgradePanel(string myName, string myDescription, int myCost, Sprite myIcon){
            name = myName;
            description = myDescription;
            cost = myCost;
            icon = myIcon;
        }

        public Sprite getIcon(){
            return icon;
        }

        public int getCost(){
            return cost;
        }

        public string getDescription(){
            return description;
        }

        public string getName(){
            return name;
        }
    }

    public void generatePanelChoice(int index){
        upgradePanel[] tempPanelArray = makePanelArray(); //makes an array of all the panels
        abilityIcon.sprite = tempPanelArray[index].getIcon(); //sets ability icon to current panel from the array
        nameText.text = tempPanelArray[index].getName();
        costText.text = "Power Cost: " + tempPanelArray[index].getCost();
        descriptionText.text = tempPanelArray[index].getDescription();
    }

    public void revealUpgrade(int index){
        generatePanelChoice(index);
        gameObject.SetActive(true); //reveals ability selection screen
        pauseGame();
    }

    void selectUpgrade(){
        unlockUpgrade(nameText.text);
        otherUpgrade.deactivate();
        gameObject.SetActive(false);
        resumeGame();
    }

    void unlockUpgrade(string name){ //takes in a name and unlocks the corresponding abililty
        int index = getIndex(nameArray, name);
        if (name == "Ion Beam"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Beam);
        } else if (name == "Laser Shield"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Shield);
        } else if (name == "Speed Roll"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Roll);
        } else if (name == "Quick Target"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Target);
        } else if (name == "Nano Bot Armor"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Heal);
        } else if (name == "Nano Bot Armor"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Heal);
        } else if (name == "Nano Bot Armor"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Heal);
        }
        nameArray = removeIndex(nameArray, index);
        desArray = removeIndex(desArray, index);
        costArray = removeIndex(costArray, index);
        spriteArray = removeIndex(spriteArray, index);
        otherUpgrade.nameArray = nameArray;
        otherUpgrade.desArray = desArray;
        otherUpgrade.costArray = costArray;
        otherUpgrade.spriteArray = spriteArray;
    }

    public upgradePanel[] makePanelArray(){ //returns an array of all the upgrade panel data
        upgradePanel[] tempPanelArray = new upgradePanel[spriteArray.Length]; //makes an upgrade panel array of size equal to the num sprites
        for (int i = 0; i < tempPanelArray.Length; i++)
        {
            upgradePanel tempPanel = new upgradePanel(nameArray[i], desArray[i], costArray[i], spriteArray[i]);
            tempPanelArray[i] = tempPanel;
        }
        return tempPanelArray;
    }

    int randomNum(int low, int high){ //makes random number from low to high
        System.Random rd = new System.Random();
        int num = rd.Next(low, high);
        return num;
    }

    T[] removeIndex<T>(T[] array, int index){ //returns a version of the array minus the given index
        T[] newArray = new T[array.Length-1];
        int newIndex = 0;
        for (int i=0; i<array.Length; i++){
            if (i != index){
                newArray[newIndex] = array[i];
                newIndex++;
            }
        }
        return newArray;
    }

    int getIndex(string[] array, string name){
        for (int i=0; i<array.Length; i++){
            if (array[i] == name){
                return i;
            }
        }
        return -1;
    }

    void deactivate(){
        gameObject.SetActive(false);
    }

    void pauseGame(){
        Time.timeScale = 0;
    }

    void resumeGame(){
        Time.timeScale = 1;
    }

    void Start(){
        deactivate();
    }
}
