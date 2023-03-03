using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;

// Script modified by ChatGPT, adding a feature to recurse through the children of all objects and modify their mesh files in-place.
// Original author is IndieChest
// Downloaded from https://assetstore.unity.com/packages/tools/modeling/mesh-optimizer-154517?aid=1101l95hL&utm_campaign=unity_affiliate&utm_medium=affiliate&utm_source=partnerize-linkmaker

[CustomEditor(typeof(OptimizeMesh))]
public class OptimizeMeshEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        OptimizeMesh myTarget = (OptimizeMesh)target;

        if (GUILayout.Button("Optimize Mesh!"))
        {
            myTarget.DecimateMesh();
        }

        if (GUILayout.Button("Optimize Meshes of Children!"))
        {
            Debug.Log("Optimizing Meshes Recursively...");
            myTarget.DecimateMeshRecursive(myTarget.transform);
            Debug.Log("Optimizing Meshes Completed!");
        }
    }
}
#endif

[ExecuteInEditMode]
public class OptimizeMesh : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    [SerializeField] float _quality = 0.5f;

    private MeshFilter _renderer;
    private Mesh _mesh;

    void Start()
    {
        _renderer = GetComponent<MeshFilter>();
        if(_renderer) _mesh = _renderer.sharedMesh;
    }

#if UNITY_EDITOR
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            DecimateMesh();
        }
    }

    public void DecimateMesh()
    {
        if (!EditorApplication.isPlaying)
        {
            var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
            meshSimplifier.Initialize(_mesh);
            meshSimplifier.SimplifyMesh(_quality);
            var destMesh = meshSimplifier.ToMesh();
            _renderer.sharedMesh = destMesh;

            UpdateMeshFile(_renderer.sharedMesh);
        }
    }

    public void DecimateMeshRecursive(Transform parent)
    {
        if (!EditorApplication.isPlaying)
        {
            if(_mesh) DecimateMesh();

            foreach (Transform child in parent)
            {
                var childRenderer = child.GetComponent<MeshRenderer>();
                if (childRenderer != null)
                {
                    var meshFilter = child.GetComponent<MeshFilter>();
                    if (meshFilter != null)
                    {
                        var meshSimplifier = new UnityMeshSimplifier.MeshSimplifier();
                        meshSimplifier.Initialize(meshFilter.sharedMesh);
                        meshSimplifier.SimplifyMesh(_quality);
                        var destMesh = meshSimplifier.ToMesh();
                        meshFilter.sharedMesh = destMesh;

                        UpdateMeshFile(meshFilter.sharedMesh);
                    }
                }

                DecimateMeshRecursive(child);
            }
        }
    }

    private void UpdateMeshFile(Mesh mesh)
    {
        var path = AssetDatabase.GetAssetPath(mesh);
        if (!string.IsNullOrEmpty(path))
        {
            MeshSaverEditor.SaveMesh(mesh, path, true, false);
        }
    }
#endif
}
