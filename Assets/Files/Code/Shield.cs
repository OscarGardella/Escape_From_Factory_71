using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public Transform objectToFollow;
    //public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = objectToFollow.position; //+ offset;
    }

    public void ActivateShield(float time){
        gameObject.SetActive(true);
        StartCoroutine(ShieldTimer(time));
    }

    IEnumerator ShieldTimer(float time){ //turns the shield on for a few seconds
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
