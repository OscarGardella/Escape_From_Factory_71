using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/*using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;*/

/* This is a class to manage a DynamicLoader object. Put the DynamicLoader's bounding box around an area of space.
   When the tracked object enters its space, the DynamicLoader will load the specified asset bundle file, and when
   it leaves the space, it unloads it from the scene. Don't forget to use the editor script to rebuild asset bundle
   files after changing the objects.*/


public class DynamicLoader : MonoBehaviour
{
  private Transform boundingBox;
  private Transform managedObj;
  public Transform trackingObj; // The object to track for loading / unloading items (the player)
  public string bundlePath; // The path to the asset bundle
  private AssetBundle bundle = null; // The asset bundle
  // Start is called before the first frame update
  void Start() {
    boundingBox = getBoundingBox();
  }

  // Retrieves the bounding box that describes over what area the object will be loaded when the player is within.
  Transform getBoundingBox() {
    Transform box = transform.Find("BoundingBox");
    if(! box) Debug.Log("dynamicLoader.cs: Error: unable to find BoundingBox in children. Please ensure a cube named BoundingBox is a child of the dynamicLoader.");
    return box;
  }
  
  Vector3 getPosition() {
    return gameObject.transform.position;
  }

  
  public void loadAssets() {
    if(bundle) return;
    Vector3 pos = getPosition();
    Debug.Log("DynamicLoader.cs \"" + this + "\": Loading assetbundle at position " + pos + ", from path " + bundlePath);
    if(! File.Exists(bundlePath)) {
      throw new FileNotFoundException("DynamicLoader.cs: Error: Unable to load asset bundle at path: " + bundlePath);
    }
    bundle = AssetBundle.LoadFromFile(bundlePath);
    Object[] assets = bundle.LoadAllAssets();
    // Note that if there are multiple assets, they will be on top of each other. This only really supports single-asset
    // asset bundles, but this is there to prevent memory leaks and weird duplication possibilities.
    foreach(GameObject asset in assets) {
      Instantiate(asset, gameObject.transform);
      //obj.transform.position = pos;
    }
  }  

  public void unloadAssets() {
    if(! bundle) return;
    Debug.Log("DynamicLoader.cs \"" + this + "\": Unloading asset bundle...");
    bundle.Unload(true);
    bundle = null;
  } 

  // Called by BoundingBox
  public void ChildTriggerEnter(Collider other) {
    if(other.gameObject == trackingObj.gameObject) {
      loadAssets();
    }
  }

  // Called by BoundingBox
  public void ChildTriggerLeave(Collider other) {
     if(other.gameObject == trackingObj.gameObject) {
      unloadAssets();
    }
  }
  // Update is called once per frame
  /*void Update()
  {
    
  }*/
}
