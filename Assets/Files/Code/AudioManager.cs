using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks; // UniTask (https://github.com/Cysharp/UniTask)

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public Transform playerCam; // Justin


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        PlayMusic("Background");
        
        // Justin - retrieve player camera to compensate for unwanted rolloff from camera height above player
        GameObject pObj = GameObject.FindGameObjectWithTag("Player");
        if(!pObj) { Debug.Log("AudioManager.cs: Failed to retrieve Player object by tag. Cannot apply spatial audio z compensation");
            return;}
        RobotFreeAnim player = pObj.GetComponent<RobotFreeAnim>();
        if(!player) { Debug.Log("AudioManager.cs: Failed to retrieve Player component. Cannot apply spatial audio z compensation");
            return;}
        playerCam = player.mainCamera.transform;
         if(!playerCam) { Debug.Log("AudioManager.cs: Failed to retrieve Player camera. Cannot apply spatial audio z compensation");
            return;}
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();               
        }
    }

    async UniTask playSpatial(AudioClip clip, Vector3 position, float spatialBlend, float distance) {
      GameObject noiseMaker = new GameObject();
      AudioSource noiseSource = noiseMaker.AddComponent<AudioSource>();
      noiseSource.spatialBlend = spatialBlend;
      noiseSource.minDistance = distance;
      noiseSource.maxDistance = distance;
      noiseSource.volume = sfxSource.volume;
      float camHeight = 25;
      if(playerCam) camHeight = playerCam.transform.position.y;
      // Make it the same height as the camera to ignore y axis, as this is a 2D game
      noiseMaker.transform.position = new Vector3(position.x, camHeight, position.z);
      noiseSource.PlayOneShot(clip);
      await UniTask.Delay((int) (clip.length*1000));
      Destroy(noiseMaker);
    }

    public void PlaySFXSpatial(string name, Vector3 position, float spatialBlend, int distance) {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null) Debug.Log("AudioManager.cs: PlaySFXSpatial: Sound \"" + name + "\" not found");
        _ = playSpatial(s.clip, position, spatialBlend, distance);
    }

    // A wrapper function for default parameters of PlaySFXSpatial, as a Vector3 nonprimitive cannot be a default argument
    /*public void PlaySFX(string name) {
        PlaySFXSpatial(name, new Vector3(0,0,0), 0, 20); // Default to sound that is global
    }*/

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("AudioManager.cs: Sound \"" + name + "\" not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
            
    }

    public void RepeatRolling()
    {
        InvokeRepeating("Rolling", 0.1f, 1.090f);
        
    }

    public void Rolling()
    {
        PlayRolling("Rolling");
    }

    public void PlayRolling(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void StopRolling()
    {
        CancelInvoke("Rolling");
    }
    
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute;
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }

    public void sfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
}

