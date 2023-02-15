using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour {
  
  TMP_Text textMesh;
  float timerOffset = 0.0f;
  public bool timerEnabled; // Enables / disables the timer

  // Start is called before the first frame update
  void Start() {
    textMesh = GetComponent<TMP_Text>();
    if(!textMesh) Debug.Log("ScoreKeeper.cs: Error: Failure retrieving TMP_Text component. Make sure this script is attached to a TextMesh.");
  }

  public void resetTimer() {
    timerOffset = Time.realtimeSinceStartup;
    textMesh.text = "Elapsed Time: 0 seconds";
  }

  // Update is called once per frame
  void Update() {
    if(! timerEnabled) return;
    textMesh.text = "Elapsed Time: " + (Time.realtimeSinceStartup - timerOffset).ToString("0.00") + " seconds";
  }
}
