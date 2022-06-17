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

    // Debugging
    List<Vector3> testPoints;
    int counter = 0;


    // Start is called before the first frame update
    void Start()
    {

        // Create Iris
        iris = new Iris(irisData);

        // Debugging
        if (!debug) return;

        testPoints = new List<Vector3>();
        for (int i = 0; i < 10; i++)
        {
            testPoints.Add(util.RandomVector(2));
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Manage Iris
        iris.Extrude();
        iris.DebugDraw();

        // Debugging
        if (!debug) return;

        for (int i = 0; i < testPoints.Count; i++)
        {
            Debug.DrawLine(irisData.origin, testPoints[i], Color.white);
        }
    }

    VertexPath ReturnSmooth(Vector3[] points)
    {
        BezierPath bezierPath = new BezierPath(points, false, PathSpace.xyz);
        Transform transform = new GameObject().transform;
        return new VertexPath(bezierPath, transform);
    }
}