using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(playerRotate());
    }

    private void Update()
    {
        StartCoroutine("playerRotate");
    }

    // Update is called once per frame
    IEnumerator playerRotate()
    {
        yield return new WaitForSeconds(3);
        transform.Rotate(new Vector3(0f, 20f, 0f) * Time.deltaTime);
    }
}
