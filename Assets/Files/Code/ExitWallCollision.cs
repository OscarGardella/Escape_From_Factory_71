using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class ExitWallCollision : MonoBehaviour
{
    public GameSuccessCanvas SuccessScreen;
    public GameObject StageClearUI;
    private int count = 0;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && SuccessScreen)
        {
            SuccessScreen.ShowUI();
        }

        else if (other.gameObject.tag == "Player" && StageClearUI && count == 0)
        {
            count = 1;
            showClearMes();
        }
    }

    private async Task showClearMes()
    {
        StageClearUI.SetActive(true);
        AudioManager.Instance.PlaySFX("Stage Complete");
        await Task.Delay(4000);
        StageClearUI.SetActive(false);
    }


}
