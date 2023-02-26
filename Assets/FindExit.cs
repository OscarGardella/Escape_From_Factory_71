using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        ShowFindExit();
    }

    // Update is called once per frame
    IEnumerator ShowFindExit()
    {
        RobotFreeAnim player = GameObject.FindGameObjectWithTag("Player").GetComponent<RobotFreeAnim>();
        while (player.controls.controlsEnabled != true)
        {
            //do nothing
        }
        gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
    }
}
