using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSoundBar : MonoBehaviour
{
    private void playSFX()
    {
        AudioManager.Instance.PlaySFX("Player Shooting");
    }
}
