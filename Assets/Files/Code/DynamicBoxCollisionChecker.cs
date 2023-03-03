using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicBoxCollisionChecker : MonoBehaviour
{
  // Start is called before the first frame update
  void Start() {
    
  }

  // Send trigger event to parent function call
  private void OnTriggerEnter(Collider other) {
    gameObject.GetComponentInParent<DynamicLoader>().ChildTriggerEnter(other);
  }
  
  private void OnTriggerExit(Collider other) {
    gameObject.GetComponentInParent<DynamicLoader>().ChildTriggerLeave(other);
  }

  // Update is called once per frame
  void Update()
  {
    
  }
}
