using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreResult : MonoBehaviour
{
    //public TMP_Text textMesh;
    public ScoreKeeper sk;
    public static int score;
    public Text TextScoreUI;

    void Start()
    {
        TextScoreUI = GameObject.Find("Score Text").GetComponent<Text>();  // if you want to reference it by code - tag it if you have several texts
        sk = GameObject.Find("ScoreDisplay").GetComponent<ScoreKeeper>();
        if (!TextScoreUI) Debug.Log("ScoreKeeper.cs: Error: Failure retrieving TextScoreUI component. Make sure this script is attached to a TextMesh.");
        if (!sk) Debug.Log("ScoreKeeper.cs: Error: Failure retrieving sk component. Make sure this script is attached to a TextMesh.");
    }

    // Start is called before the first frame update
    void Update()
    {
        //textMesh = GetComponent<TMP_Text>();
        score = sk.getScore();
        TextScoreUI.text = score.ToString();
        //TextScoreUI.text = "aaa";
    }
}
