using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IrisCreator : MonoBehaviour
{
    public IrisSettings irisSet;

    [HideInInspector]
    Iris iris;
    Mesh mesh;
    MeshFilter meshFilter;


    // Start is called before the first frame update
    void Start()
    {
        // Create Iris
        iris = new Iris(irisSet, gameObject.transform);

        // Setup Mesh
        meshFilter = GetComponent<MeshFilter>();
    }

    // Update is called once per frame
    void Update()
    {
        // Update Iris
        iris.Update();
        iris.DebugDraw();

        // Check for input
        if (Input.GetKeyDown("space"))
        {
            mesh = iris.GenerateMesh();
            meshFilter.mesh = mesh;
        }
    }
}