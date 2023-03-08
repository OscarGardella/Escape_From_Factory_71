using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/*using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;*/
using UnityEngine.AddressableAssets;

/* This is a class to manage a DynamicLoader object. Put the DynamicLoader's bounding box around an area of space.
   When the tracked object enters its space, the DynamicLoader will load the specified asset bundle file, and when
   it leaves the space, it unloads it from the scene. Don't forget to use the editor script to rebuild asset bundle
   files after changing the objects.*/


public class DynamicLoader : MonoBehaviour
{
  private Transform boundingBox;
  private GameObject managedObj;
  public Transform trackingObj; // The object to track for loading / unloading items (the player)
  public float rotation = 0; // Loaded object rotation relative to the DynamicLoader

  [SerializeField] private AssetReferenceGameObject reference;

  // Start is called before the first frame update
  void Start() {
    boundingBox = getBoundingBox();
    if(reference == null) {
      Debug.Log("DynamicLoader.cs: " + this + "Addressable reference not set.");
    }
    //loadAssets();
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

  // Note that this function is a last minute hack for my specific implementation...
  private void markDoneLoading() {
    GameObject hack = GameObject.FindGameObjectWithTag("Hack");
    if(!hack) { Debug.Log("PlayButton: Unable to find hack text"); return; }
    OpeningAnimHackText hackAnim = hack.GetComponent<OpeningAnimHackText>();
    if(!hackAnim) { Debug.Log("PlayButton: Unable to find hack text component"); return; }
    hackAnim.isLoading = false;
  }

  private void OnLoadDone(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj) {
    Debug.Log("Load done. Attempting to set managed object...");
     
    managedObj = obj.Result;
    managedObj.transform.parent = gameObject.transform.parent; // Reparent the object to the parent of the DynamicLoader
    //managedObj.transform.position = new Vector3(0,0,0);
    if(managedObj == null) Debug.Log("DynamicLoader.cs: OnLoadDone: obj.Result is null. Will not be able to destroy object later");
    Debug.Log("Load done. Set managed object");
    markDoneLoading();
  }

  public void loadAssets() {
    if(reference == null) return;
    Debug.Log("DynamicLoader.cs \"" + this + "\": Loading addressable asset...");
    Quaternion rot = gameObject.transform.rotation;
    rot.y += rotation;
    reference.InstantiateAsync(getPosition(), rot).Completed += OnLoadDone;
  }

  public void unloadAssets() {
    //Debug.Log("DynamicLoader.cs \"" + this + "\": Was called to unload addressable asset...");
    //if (reference == null || !reference.IsValid()) return;
    Debug.Log("DynamicLoader.cs \"" + this + "\": Unloading addressable asset...");
    GameObject assetInstance = reference.Asset as GameObject;
    if (assetInstance != null) {
        Debug.Log("Releasing assetInstance...");
        Addressables.ReleaseInstance(assetInstance);
        GameObject.Destroy(assetInstance);
    }
    reference.ReleaseAsset();
    Debug.Log("Destroying object...");
    Destroy(managedObj);
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

  // Update is called once per frame
  /*void Update()
  {
    
  }*/
}