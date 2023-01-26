using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameSuccessCanvas : MonoBehaviour
{
    [SerializeField] Text message;


    public GameObject player;
    public GameObject ExitSign;

    //private PlayerHealth health_left;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void RevealSuccess()
    {
        gameObject.SetActive(true);// show the screen
    }
}
