using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class DynamicLoaderBundler : EditorWindow
{
  //public string[] dynamicLoaderPaths;
  //static AssetBundleBuild[] buildMap;
  // Start is called before the first frame update
  //void Start() {
  private static AssetBundleBuild[] getBuildMap() {
    AssetBundleBuild[] buildMap;
    string[] dynamicLoaderPaths = new string[] {"Assets/Prefabs/Scene_Prefabs/Stage_1.prefab", "Assets/Prefabs/Scene_Prefabs/Stage_2.prefab", "Assets/Prefabs/Scene_Prefabs/Stage_3.prefab"};
    //string g[] = {"Assets/Prefabs/Scene_Prefabs/Stage1.prefab"};
    buildMap = new AssetBundleBuild[dynamicLoaderPaths.Length];
    // Populate buildMap with asset paths
    for(int i = 0; i < dynamicLoaderPaths.Length; ++i) {
      buildMap[i] = new AssetBundleBuild();
      buildMap[i].assetBundleName = Path.GetFileNameWithoutExtension(dynamicLoaderPaths[i]);
      buildMap[i].assetNames = new string[] {dynamicLoaderPaths[i]};
      //new AssetBundleBuild("name", dynamicLoaderPaths[i]);
    }
    //Debug.Log("AssetBundleBuild map: " + buildMap)
    return buildMap;
  }

[MenuItem("Assets/Build AssetBundles")]
  public static void buildAssetBundle() {
    //Debug.Log(Object.FindObjectsOfType<DynamicLoader>().Length);
    //Scene scene = EditorSceneManager.GetActiveScene();
   
    // This was supposed to find the prefab path of what the DynamicLoaders want to manage.
    // I was hoping it could do it more automatically, finding each DynamicLoader in the scene, and creating
    // asset bundles that can be easily found by them. However, editor scripts cannot know any symbols relating
    // to classes for objects outside the Editor folder. Honestly it's probably simpler
    // keeping the building and loading separated and not integrated for now.
    //Debug.Log("Finding DynamicLoader objects...");
    //Debug.Log(GameObject.FindGameObjectsWithTag("DynamicLoader").Length);

    //foreach(GameObject genericLoader in GameObject.FindGameObjectsWithTag("DynamicLoader")) {
      //Debug.Log( AssetDatabase.GetAssetPath(genericLoader));
      //Debug.Log("Editor script loader hash for " + genericLoader + ": " + genericLoader);
      //DynamicLoader loader = genericLoader.GetComponent<DynamicLoader>();
      //Debug.Log("Loader: " + loader.getBoundingBox());
      /*if(! loader) Debug.Log("dynamicLoader.cs: Error: A DynamicLoader object could not be found.");
      //Debug.Log("Loader box: " + loader.getBoundingBox());
      GameObject prefabObject = PrefabUtility.GetCorrespondingObjectFromSource(loader.getObject().gameObject);
      Object actualPrefab = PrefabUtility.GetPrefabInstanceHandle(prefabObject);
      //Debug.Log(actualPrefab);
      Debug.Log( AssetDatabase.GetAssetPath( AssetDatabase.GetAssetPath(loader.getObject())));
      //Debug.Log("Loader object: " + loader.getObject());
      //Debug.Log("Prefabbbb: " + PrefabUtility.GetPrefabObject(prefabObject));
      ////Debug.Log("Loader object: " + prefabObject);
      //GameObject pre = PrefabUtility.GetOutermostPrefabInstanceRoot(prefabObject); 
      ////Debug.Log("Asset path of " + prefabObject.name + ": " + AssetDatabase.GetAssetPath(actualPrefab));*/
    //}
    
    Debug.Log("Building assetbundles...");
    AssetBundleBuild[] buildMap = getBuildMap();
    string assetBundleDir = "AssetBundles";
    string relativeDir = Application.dataPath + "/" + assetBundleDir;
    // Create asset bundle directory if doesn't exist
    if(!Directory.Exists(relativeDir)) {
      Directory.CreateDirectory(relativeDir);
    }
    // TODO: What? How do you specify which particular assets you actually want to build?
    BuildPipeline.BuildAssetBundles(relativeDir, buildMap, BuildAssetBundleOptions.None, BuildTarget.WebGL);
    Debug.Log("Completed!");
  }


  // Update is called once per frame
  /*void Update()
  {
    
  }*/
}
