using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PandoraUtils;

public class Iris
{
    // properties
    List<IRPath> paths;
    static float step;
    static float currentRadius;

    //statics
    static IrisData irisData;

    // cunstructor
    public Iris(IrisData _irisDat)
    {
        irisData = _irisDat;
        step = irisData.totalRadius / irisData.radialSteps;
        currentRadius = 0f;
        paths = new List<IRPath>();
        InitCircle();
    }

    // methods
    public void InitCircle()
    {
        for (int i = 0; i < irisData.numInitPoints; i++)
        {
            paths.Add(new IRPath(util.RandomCircleCoordinate(.3f)));
        }
    }

    public void Extrude()
    {
        foreach (var IRPath in paths)
        {
            //if (currentRadius + step > irisData.totalRadius) return;
            IRPath.Extrude();
            IRPath.Displace();
        }
    }

    public void DebugDraw()
    {
        foreach (IRPath IRPath in paths)
        {
            IRPath.DebugDraw();
            //Debug.Log("current paths: " + paths.Count);
        }
    }

    public void CirclePoints()
    {

    }

    public class IRPath
    {
        // structs
        struct IRPoint
        {
            public Vector3 position;
            public float radius;
        }

        // properties
        List<IRPoint> points;
        bool alive;
        int pathID;

        // statics
        static int Count;

        // constructor
        public IRPath(Vector3 start)
        {
            alive = true;
            pathID = Count;
            points = new List<IRPoint>();
            AddPoint(start);
            Count++;
        }

        // methods
        void AddPoint(Vector3 point)
        {
            points.Add(new IRPoint { position = point, radius = GetRadius(point) });
        }

        float GetRadius(Vector3 point)
        {
            //Debug.Log("Radius");
            float distToOrigin = Vector3.Distance(point, irisData.origin);
            Color sampleGradient = irisData.stageDistribution.Evaluate(distToOrigin);
            Color.RGBToHSV(sampleGradient, out float h, out float s, out float v);
            float radius = v * irisData.maxPointRadius;
            return radius;
        }

        void Kill()
        {
            alive = false;
        }

        public void Extrude()
        {
            Vector3 position =  points[points.Count - 1].position;
            Vector3 displacement = (position - irisData.origin).normalized * step;
            AddPoint(position + displacement);
            currentRadius += step;
            //Debug.Log("Extrude");
        }
        
        public void Displace()
        {
            /*
            // IF USING STRUCTS IN A LIST //
            .Set won't work because the actual object is just returned as a copy
            -> create new instance and replace
            */

            //Debug.Log("Displace");
            Vector3 position = points[points.Count - 1].position;
            //Debug.Log("original position: " + position);
            position += util.RandomVector(irisData.displacementLimit);
            //Debug.Log("new position: " + position);
            IRPoint point = new IRPoint { position = position, radius = points[points.Count - 1].radius };
            points[points.Count - 1] = point;
            //Debug.Log("displaced position: " + points[points.Count - 1].position);
        }

        public void DebugDraw()
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                Debug.DrawLine(points[i].position, points[i + 1].position, Color.white);
            }
            //Debug.Log(points.Count);
        }
    }
}
