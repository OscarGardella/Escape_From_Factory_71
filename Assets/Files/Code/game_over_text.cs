using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class game_over_text : MonoBehaviour
{

    [SerializeField] Text message;


    public GameObject player;
    private PlayerHealth health_left;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);

    }

    public void RevealGameOver()
    {
        gameObject.SetActive(true);// show the screen
    }

}
