using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks; // UniTask (https://github.com/Cysharp/UniTask)

public class ScoreKeeper : MonoBehaviour {
  
  TMP_Text textMesh;
  int score;
  public int fontSize = 18;
  public int growFontSize = 20;
  public int growDelay = 40; // Milliseconds

  // Start is called before the first frame update
  void Start() {
    textMesh = GetComponent<TMP_Text>();
    if(!textMesh) Debug.Log("ScoreKeeper.cs: Error: Failure retrieving TMP_Text component. Make sure this script is attached to a TextMesh.");
    resetScore();
    textMesh.fontSize = fontSize;
    textMesh.color = Color.white;
  }

  private async UniTask displayScoreUpdate() {
    textMesh.color = Color.yellow;
    textMesh.text = "Score: " + score;
    int steps = growFontSize - fontSize;
    // Get bigger
    for(int i = 0; i < steps; ++i) {
      textMesh.fontSize++;
      await UniTask.Delay(growDelay);
    }
    // Get smaller
    for(int i = 0; i < steps; ++i) {
      textMesh.fontSize--;
      await UniTask.Delay(growDelay);
    }
    textMesh.color = Color.white;
  }

  public void resetScore() {
    score = 0;
  }

  public int getScore() {
    return score;
  }
  
  public void incrScore(int moreScore) {
    score += moreScore;
    _ = displayScoreUpdate();
  }

  /*
  // Update is called once per frame
  void Update() {
    if(! timerEnabled) return;
    textMesh.text = "Elapsed Time: " + getTime().ToString("0.00") + " seconds";
  }*/
}
