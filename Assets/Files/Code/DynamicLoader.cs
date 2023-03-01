using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/*using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;*/

/* This is a class to manage a DynamicLoader object. The DynamicLoader has two items. One is a bounding box, and the other is a GameObject
   you want to manage. The object will be loaded if the player is within the bounding box, or unloaded if not.*/
public class DynamicLoader : MonoBehaviour
{
  private Transform boundingBox;
  private Transform managedObj;
  // Start is called before the first frame update
  void Start() {
    boundingBox = getBoundingBox();
    //managedObj = getObject();
  }

  // Retrieves the bounding box, if present.
  Transform getBoundingBox() {
    Transform box = transform.Find("BoundingBox");
    if(! box) Debug.Log("dynamicLoader.cs: Error: unable to find BoundingBox in children. Please ensure a cube named BoundingBox is a child of the dynamicLoader.");
    return box;
  }

  /*// Retrieves the object you want to manage, if present.
  Transform getObject() {
    Transform[] objs = GetComponentsInChildren<Transform>();
    Transform thisAsTransform = this.GetComponent<Transform>();
    Transform childObj = null;
    foreach (Transform o in objs) { // Pick the first object that isn't itself and isn't the BoundingBox
      if (o != thisAsTransform && o.name != "BoundingBox") {
        childObj = o;
        break;
      }
    }
    if(childObj == null) Debug.Log("dynamicLoader.cs: Error: cannot obtain object to manage. Please ensure the object is a child of the DynamicLoader object, as is the boundingBox.");
    return childObj;
  }*/

  

  // Update is called once per frame
  /*void Update()
  {
    
  }*/
}
