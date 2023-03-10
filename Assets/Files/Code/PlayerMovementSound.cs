using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementSound : MonoBehaviour
{
  /*public AudioSource walk1;
  public AudioSource walk2;
  public AudioSource walk3;*/
  //public AudioSource revUp;
  public AudioSource roll;
  //private AudioManager audioManager;
  
  private T fetchObjectByTag<T>(string tag) where T : Component {
    GameObject g = GameObject.FindGameObjectWithTag(tag);
    if (!g) throw new System.NullReferenceException($"Failed to find game object with tag \"{tag}\"");
    T obj = g.GetComponent<T>();
    if (!obj) throw new System.NullReferenceException($"Object with tag \"{tag}\" has no component \"{typeof(T).Name}\"");
    return obj;
  }


  // Start is called before the first frame update
  void Start() {
    //GameObject.FindGameObjectWithTag("AudioManager");
    //AudioManager audioManager = fetchObjectByTag<AudioManager>("AudioManager");
    // Note that these components must be in order in the inspector.
    /*if(! walk1) {AudioSource walk1 = GetComponents<AudioSource>()[0];}
    if(! walk2) {AudioSource walk2 = GetComponents<AudioSource>()[1];}
    if(! walk3) {AudioSource walk3 = GetComponents<AudioSource>()[2];}
    //if(! revUp) {AudioSource revUp = GetComponents<AudioSource>()[3];}
    if(! roll) {AudioSource roll = GetComponents<AudioSource>()[4];}
    
    if(! walk1) Debug.Log("PlayerWalkSound.cs: Failure getting audio component walk1 sound from player");
    else if(! walk2) Debug.Log("PlayerWalkSound.cs: Failure getting audio component walk2 sound from player");
    else if(! walk3) Debug.Log("PlayerWalkSound.cs: Failure getting audio component walk2 sound from player");
    else if(! roll) Debug.Log("PlayerWalkSound.cs: Failure getting audio component roll sound from player");*/

    // Temp
    if(! roll) {AudioSource roll = GetComponents<AudioSource>()[4];}
    if(! roll) Debug.Log("PlayerWalkSound.cs: Failure getting audio component roll sound from player");
  }

  // Update is called once per frame
  /*void Update()
  {
    
  }*/

  // Play audio clip
  public void play_Walk_Left_Foot() {
    //walk1.Play();
    if(AudioManager.Instance) AudioManager.Instance.PlaySFX("Walk Clink1");
  }

  public void play_Walk_Right_Foot() {
    //walk2.Play();
    if(AudioManager.Instance) AudioManager.Instance.PlaySFX("Walk Clink2");
  }

  public void play_Walk_Back_Foot() {
    //walk3.Play();
    if(AudioManager.Instance) AudioManager.Instance.PlaySFX("Walk Clink3");
  }

  /*public void play_Rev_Up() {
    revUp.Play();
  }*/

  public void play_Roll() {
    if(AudioManager.Instance) AudioManager.Instance.RepeatRolling();
  }

  public void stop_Roll() {
    if(AudioManager.Instance) AudioManager.Instance.StopRolling();
  }
}
