using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PathCreation;
using PandoraUtils;

public class IrisCreator : MonoBehaviour
{
    public IrisData irisData;
    public bool debug;

    [HideInInspector]
    Iris iris;


    // Start is called before the first frame update
    void Start()
    {
        // Create Iris
        iris = new Iris(irisData, debug);

        // Debugging
        if (!debug) return;
    }

    // Update is called once per frame
    void Update()
    {
        // Manage Iris
        iris.Update();

        // Debugging
        if (!debug) return;
        iris.DebugDraw();
    }

    VertexPath ReturnSmooth(Vector3[] points)
    {
        BezierPath bezierPath = new BezierPath(points, false, PathSpace.xyz);
        Transform transform = new GameObject().transform;
        return new VertexPath(bezierPath, transform);
    }
}