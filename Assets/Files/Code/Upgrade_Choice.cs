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
    public string[] nameArray = {"Beam", "Black Hole", "Roll", "Rock", "Heal"};
    public string[] desArray = {"Fire a large beam out of the front of your robot obliterating anything in your way ","??? #2","Curl into a ball and roll at high speeds to evade enemies","??? #4","Double your health and heal to full, allowing you to survive longer"};
    public int[] costArray = {5,10,5,5,0};

    public class upgradePanel
    {
        string name;
        string description;
        int cost;
        Sprite icon;
        enum AbilityName{ //names of all abilites
            Beam, Blackhole, Roll, Rock, Heal
        }

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

    public void generatePanelChoice(){
        upgradePanel[] tempPanelArray = makePanelArray(); //makes an array of all the panels
        int r = randomNum(0, tempPanelArray.Length); //generates a random index of the panel array
            abilityIcon.sprite = tempPanelArray[r].getIcon(); //sets ability icon to current panel from the array
            nameText.text = tempPanelArray[r].getName();
            costText.text = "Cost: " + tempPanelArray[r].getCost();
            descriptionText.text = tempPanelArray[r].getDescription();
    }

    public void revealUpgrade(){
        generatePanelChoice();
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
        if (name == "Beam"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Beam);
            Debug.Log("Beam unlocked");
        } else if (name == "Black Hole"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Blackhole);
            Debug.Log("Black Hole unlocked");
        } else if (name == "Roll"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Roll);
            Debug.Log("Roll unlocked");
        } else if (name == "Rock"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Rock);
            Debug.Log("Rock unlocked");
        } else if (name == "Heal"){
            abilityTracker.UnlockAbility(AbilityTracking.AbilityName.Heal);
            Debug.Log("Heal unlocked");
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

    upgradePanel[] makePanelArray(){ //returns an array of all the upgrade panel data
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
