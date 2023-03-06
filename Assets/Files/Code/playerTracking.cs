using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerTracking : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player = null;
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(player) transform.LookAt(player.transform);
    }
}
