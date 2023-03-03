using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreResult : MonoBehaviour
{
    ScoreKeeper sk;
    int score;
    Text TextScoreUI;

    void Start()
    {
        TextScoreUI = GameObject.Find("Score Text").GetComponent<Text>(); 
        sk = GameObject.Find("ScoreDisplay").GetComponent<ScoreKeeper>();
    }

    // Start is called before the first frame update
    void Update()
    {
        score = sk.getScore();
        TextScoreUI.text = score.ToString();
    }
}
