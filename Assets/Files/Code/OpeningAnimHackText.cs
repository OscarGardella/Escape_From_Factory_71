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
    textMesh.text = "I must find a body";
    await UniTask.Delay(1000);
    textMesh.text = "Here one is!";
    await UniTask.Delay(1000);
    textMesh.text = "Let me just hack it...";
    await UniTask.Delay(1000);
    textMesh.text = "Hacking...";
    await UniTask.Delay(1000);
    textMesh.text = "Hack successful.";
    await UniTask.Delay(500);
    textMesh.text = "Booting...";
  }

  // Update is called once per frame
  /*void Update()
  {
    
  }*/
}
