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

    public void update()
    {
        if ((ExitSign.transform.position - player.transform.position).magnitude < 30.0f)
        {
            gameObject.SetActive(true);// show the screen
        }
    }
}
