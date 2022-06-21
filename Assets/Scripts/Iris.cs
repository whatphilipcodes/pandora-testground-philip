using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PandoraUtils;


public class Iris
{
    #region Fields

    static IrisData irisData;
    //int circleID;
    float stepSize;
    float radialStep;
    List<IRPath> paths;

    #endregion

    #region Constructor

    public Iris(IrisData _irisData)
    {
        irisData = _irisData;
        stepSize = irisData.totalRadius / irisData.radialSteps;
        //circleID = 0;
        radialStep = 0;
        paths = new List<IRPath>();
    }

    #endregion

    #region Public Access

    public void Iterate()
    {
        foreach (var IRPath in paths)
        {
            IRPath.AddPoints(DistributePoints());
        }
    }

    #endregion

    #region Internal Methods

    internal Vector3[] DistributePoints()
    {
        int pointsAmount = GetPointsAmount(radialStep);
        if (pointsAmount == 0) return null;

        Vector3[] points = new Vector3[pointsAmount];

        for (int i = 0; i < pointsAmount; i++)
        {
            float angleStep = 360 / pointsAmount;
            float random = Random.Range(-angleStep, angleStep);
            float theta = i * angleStep + random;

            points[i] = util.CircleCoordinate(radialStep, theta);
        }

        return points;
    }

    internal int GetPointsAmount(float radius)
    {
        float weight = SampleDistributionGradient(radius);
        int amount = Random.Range((int)(irisData.minStepResolution * weight),(int)((irisData.maxStepResolution + 1) * weight));
        // + 1 since when using Random.Range on int the upper range changes into an exclusive
        return amount;
    }

    internal float SampleDistributionGradient(float t)
    {
        Color sampleGradient = irisData.weightDistribution.Evaluate(t);
        Color.RGBToHSV(sampleGradient, out float h, out float s, out float v);
        return v;
    }

    #endregion

    #region Subclasses and Helpers

    class IRPath
    {
        #region Fields

        int pathID;
        bool alive;

        struct IRPoint
        {
            int circleID;
            float cylRadius;
            Vector3 position;
        }

        List<IRPoint> points;

        #endregion

        #region Constructor

        IRPath()
        {
            alive = true;
            points = new List<IRPoint>();
            Initialize();
        }

        #endregion

        #region Methods

        void Initialize()
        {

        }

        internal void AddPoints(Vector3[] points)
        {
            if (points == null) return;


        }

        #endregion

    }
    #endregion
}


/*
public class Iris
{
    #region Fields

    // properties
    List<IRPath> paths;
    public int circleID;

    //statics
    static IrisData irisData;

    #endregion

    #region Constructors

    /// <summary>Iris object used to manage processes for creation of a unique iris mesh.</summary>

    // cunstructor
    public Iris(IrisData _irisData)
    {
        circleID = 0;
        irisData = _irisData;
        paths = new List<IRPath>();
        Initialize();
    }

    #endregion

    #region Methods

    // methods
    public void Initialize()
    {
        for (int i = 0; i < irisData.numInitPoints; i++)
        {
            paths.Add(new IRPath());
        }
    }

    public void Expand()
    {
        foreach (var IRPath in paths)
        {
            IRPath.Extrude(circleID);
        }
        circleID += 1;
    }

    public void DebugDraw()
    {
        foreach (IRPath IRPath in paths)
        {
            IRPath.DebugDraw();
        }
    }

    
    public List<IRPoint> GetNeighbors(int circleID)
    {
        List<IRPoint> results = new List<IRPoint>(); 
        foreach (var IRPath in paths)
        {
            results.Add(IRPath.GetPoint(circleID));
        }
        return results;
    }
    

    #endregion

    #region  Helper - Classes and Structs

    public class IRPath
    {
        #region Fields

        // properties
        List<IRPoint> points;
        bool alive;
        int pathID;
        float step;
        float currentRadius;

        // statics
        static int pathCount;

        #endregion

        #region Constructors

        /// <summary>An IrisPath consists of a list of IRPoints.</summary>
        /// <param name="start"> A startpoint is required for instantiation.</param>

        // constructor
        public IRPath()
        {
            alive = true;
            pathID = pathCount;
            step = irisData.totalRadius / irisData.radialSteps;
            points = new List<IRPoint>();
            AddPoint(util.RandomCircleCoordinate(2), 0);
            pathCount++;
        }

        #endregion

        #region Methods
        // methods

        /// <summary>Adds a point to the IRPath.</summary>
        /// <param name="point">Vector 3 to be added.</param>
        void AddPoint(Vector3 point, int id)
        {
            points.Add(new IRPoint(point, id));
        }

        
        public IRPoint GetPoint(int pointID)
        {
            return points[pointID];
        }

        /// <summary>Disables IRPath executed on.</summary>
        void Kill()
        {
            alive = false;
        }
        
        public void Extrude(int circleID)
        {
            Vector3 position =  points[points.Count - 1].position;
            Vector3 displacement = (position - irisData.origin).normalized * step;
            displacement += util.RandomVector(irisData.displacementLimit);
            AddPoint(position + displacement, circleID);
            currentRadius += step;
        }

        public void DebugDraw()
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                Debug.DrawLine(points[i].position, points[i + 1].position, Color.white);
            }
        }

        #endregion
    }

    public class IRPoint
    {
        #region Fields

        public Vector3 position;
        public float radius;
        public int circleID;

        #endregion

        #region Statics

        static int pointCount;

        #endregion

        #region Constructor

        /// <summary>An IrisPoint consists of a position (Vector3), radius (float) and ID (int). </summary>
        public IRPoint (Vector3 _pos, int _circleID)
        {
            position = _pos;
            radius = GetRadius(_pos);
            circleID = _circleID;
            pointCount++;
        }

        #endregion

        #region Methods

        /// <summary>Returns radius based on distribution gradient.</summary>
        float GetRadius(Vector3 point)
        {
            //Debug.Log("Radius");
            float distToOrigin = Vector3.Distance(point, irisData.origin);
            Color sampleGradient = irisData.stageDistribution.Evaluate(distToOrigin);
            Color.RGBToHSV(sampleGradient, out float h, out float s, out float v);
            float radius = v * irisData.maxPointRadius;
            return radius;
        }

        #endregion
    }

    #endregion
}
*/