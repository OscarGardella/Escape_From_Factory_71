using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System.Threading.Tasks;

public class ExitWallCollision : MonoBehaviour
{
    public GameObject StageClearUI;
    private int count = 0;

    void OnCollisionEnter(Collision other)
    {
        GameObject parentObject = GameObject.Find("UI");
        Transform childTransform = parentObject.transform.Find("GameSuccessUI");
        GameObject successScreen1 = childTransform.gameObject;
        GameSuccessCanvas successScreen = successScreen1.GetComponent<GameSuccessCanvas>();

        if (other.gameObject.tag == "Player" && gameObject.tag == "ExitWall")
        {
            successScreen.ShowUI();
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
