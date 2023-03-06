using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explodeOnDeath : MonoBehaviour
{
    [SerializeField]
    public GameObject explosion;
    // Start is called before the first frame update
    void OnDestroy()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
    }
}
