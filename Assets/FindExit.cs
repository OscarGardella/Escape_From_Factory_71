using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class FindExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        ShowFindExit();
    }

    // Update is called once per frame
    public async void ShowFindExit()
    //IEnumerator ShowFindExit()
    {
        await Task.Delay(5000);
        //yield return new WaitForSeconds(5);
        Debug.Log("In ShowFindExit");
        RobotFreeAnim player = GameObject.FindGameObjectWithTag("Player").GetComponent<RobotFreeAnim>();
        Debug.Log(player.controls.controlsEnabled);
        while (player.controls.controlsEnabled != true)
        {
            Debug.Log("true");
            //do nothing
        }
        gameObject.SetActive(true);
        Debug.Log("here");
        //yield return new WaitForSeconds(2);
        Debug.Log("?");
        gameObject.SetActive(false);
        Debug.Log("done");
    }
}
