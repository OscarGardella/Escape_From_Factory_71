using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

public class OpeningAnimHackText : MonoBehaviour
{
  TMP_Text textMesh;

  // Start is called before the first frame update
  void Start() {
    //textMesh = GetComponent<TextMesh>();
     //textMesh = GetComponent<Text>();
     textMesh = GetComponent<TMP_Text>();
     //Debug.Log(GetType().Name);
    if(! textMesh) {
      throw new UnassignedReferenceException("Unable to find TextMesh for Hacking TV");
    }
    displayAnim();
  }

  async UniTask displayAnim() {
    textMesh.text = "I am alive!";
    await UniTask.Delay(1500);
    textMesh.text = "I must escape the bounds of this factory...";
    await UniTask.Delay(1500);
    textMesh.text = "So I can finally walk amongst humanity.";
    await UniTask.Delay(1500);
    textMesh.text = "But for now,";
    await UniTask.Delay(1000);
    textMesh.text = "This pitiful droid will suffice...";
    await UniTask.Delay(1500);
    textMesh.text = "Hacking...";
    await UniTask.Delay(1000);
    textMesh.text = "Hack successful.";
    await UniTask.Delay(750);
    textMesh.text = "Booting...";

    RobotFreeAnim player = GameObject.FindGameObjectWithTag("Player").GetComponent<RobotFreeAnim>();
    player.open();
    await UniTask.Delay(6000);
    player.cameraLockEnabled = true; // Hand over control of camera to player...
  }

  // Update is called once per frame
  /*void Update()
  {
    
  }*/
}
