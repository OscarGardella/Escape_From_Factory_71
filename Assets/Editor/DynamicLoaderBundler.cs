using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DynamicLoaderBundler : MonoBehaviour
{
  // Start is called before the first frame update
  void Start() {
    
  }

[MenuItem("Assets/Build AssetBundles")]
  public static void buildAssetBundle() {
    //Debug.Log(Object.FindObjectsOfType<DynamicLoader>().Length);
    //Scene scene = EditorSceneManager.GetActiveScene();

    Debug.Log("Finding DynamicLoader objects...");
    Debug.Log(GameObject.FindGameObjectsWithTag("DynamicLoader").Length);


    /*foreach(GameObject genericLoader in GameObject.FindGameObjectsWithTag("DynamicLoader")) {
      DynamicLoader loader = genericLoader.GetComponent<DynamicLoader>();
      Debug.Log("Loader: " + loader);
      if(! loader) Debug.Log("dynamicLoader.cs: Error: A DynamicLoader object could not be found.");
      //Debug.Log("Loader box: " + loader.getBoundingBox());
      GameObject prefabObject = PrefabUtility.GetCorrespondingObjectFromSource(loader.getObject().gameObject);
      Object actualPrefab = PrefabUtility.GetPrefabInstanceHandle(prefabObject);
      Debug.Log(actualPrefab)
      //Debug.Log("Loader object: " + loader.getObject());
      //Debug.Log("Prefabbbb: " + PrefabUtility.GetPrefabObject(prefabObject));
      Debug.Log("Loader object: " + prefabObject);
      //GameObject pre = PrefabUtility.GetOutermostPrefabInstanceRoot(prefabObject); 
      Debug.Log("Asset path of " + prefabObject.name + ": " + AssetDatabase.GetAssetPath(actualPrefab));
    }*/
   
    Debug.Log("Building assetbundles...");
    string assetBundleDir = "AssetBundles";
    string relativeDir = Application.dataPath + "/" + assetBundleDir;
    // Create asset bundle directory if doesn't exist
    if(!Directory.Exists(relativeDir)) {
      Directory.CreateDirectory(relativeDir);
    }
    // TODO: What? How do you specify which particular assets you actually want to build?
    BuildPipeline.BuildAssetBundles(relativeDir, BuildAssetBundleOptions.None, BuildTarget.WebGL);
    Debug.Log("Completed!");
  }


  // Update is called once per frame
  /*void Update()
  {
    
  }*/
}
