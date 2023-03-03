using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cysharp.Threading.Tasks;

public class OpeningAnimHackText : MonoBehaviour
{
  TMP_Text textMesh;
  private bool played; // True when the opening animation is currently playing

  // Start is called before the first frame update
  void Start() {
    //textMesh = GetComponent<TextMesh>();
     //textMesh = GetComponent<Text>();
     textMesh = GetComponent<TMP_Text>();
     //Debug.Log(GetType().Name);
    if(! textMesh) {
      throw new UnassignedReferenceException("Unable to find TextMesh for Hacking TV");
    }
    //_ = displayAnim();
    played = false;

  }

  private async UniTask waitForMouseClick() {
    await UniTask.WaitUntil( () => Input.GetMouseButtonDown(0) == true);
  }
  
  public bool hasPlayed() {
    return played;
  }

  public void showAnim(){
    played = false;
    pauseGame.paused = false; // Unpause to show animation
      _ = displayAnim();
  }

  public void resetAnim() {
    played = false;
  }

  public async UniTask displayAnim() {
    RobotFreeAnim player = GameObject.FindGameObjectWithTag("Player").GetComponent<RobotFreeAnim>();
    if(! player) {
      Debug.Log("OpeningAnimHackText.cs: Error: unable to find RobotFreeAnim Player object via tag \"Player\"");
      return;
    }
    
    player.controls.controlsEnabled = false; // Disable player controls while opening animation is playing

    Camera playerCam = player.mainCamera;
    if(playerCam == null) {
      Debug.Log("OpeningAnimHackText.cs: Error: player does not have a camera");
      return;
    }


    // Activate camera flyout animation, if the animation controller is attached.
    Animator playerCamAnim = playerCam.GetComponent<Animator>();
    if(! playerCamAnim) {
      Debug.Log("OpeningAnimHackText.cs: Error: player camera does not have an animation controller. Unable to activate camera animations. >>> This script will probably crash. <<<");
    }

    playerCamAnim.SetBool("Start", true); // Begin opening animation

    textMesh.text = "I am alive! (Click to continue)";
    //await UniTask.Delay(1500);
    await waitForMouseClick();
    textMesh.text = "I must escape the bounds of this factory...";
    //await UniTask.Delay(1500);
    await waitForMouseClick();
    textMesh.text = "So I can finally walk amongst humanity.";
    //await UniTask.Delay(1500);
    await waitForMouseClick();
    textMesh.text = "But for now,";
    //await UniTask.Delay(1000);
    await waitForMouseClick();
    textMesh.text = "This pitiful droid will suffice...";
    //await UniTask.Delay(1500);
    await waitForMouseClick();
    textMesh.text = "Hacking...";
    await UniTask.Delay(1000);
    textMesh.text = "Hack successful.";
    await UniTask.Delay(750);
    textMesh.text = "Booting...";
    player.open(); // Turn on the player

    await UniTask.Delay(1000);
    playerCamAnim.SetBool("Start", false);
    playerCamAnim.SetBool("PlayFlyup", true); // Camera flys up above player
    
    await UniTask.Delay(4000);
    player.cameraLockEnabled = true; // Hand over control of camera to player...
    // Now attempt to retrieve and start the score timer
    GameObject scoreDisplay = GameObject.FindGameObjectWithTag("ScoreDisplay");
    if(! scoreDisplay) Debug.Log("OpeningAnimHackText.cs: Error: Failed to find Score Display text box by tag \"ScoreDisplay\"");
    ScoreKeeper score = scoreDisplay.GetComponent<ScoreKeeper>();
    if(!score) {
      Debug.Log("OpeningAnimHackText.cs: Error: unable to find ScoreKeeper object via tag \"ScoreDisplay\". Cannot start timer");
    } else {
      score.enabled = true;
    }
    player.controls.controlsEnabled = true; // reenable player controls
    played = true;
    textMesh.text = ""; // Hide text
    playerCamAnim.SetBool("PlayFlyup", false);
  }

  // Update is called once per frame
  /*void Update()
  {
    
  }*/
}
