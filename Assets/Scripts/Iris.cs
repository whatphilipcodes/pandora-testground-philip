using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using PandoraUtils;

public class Iris
{
    #region Properties
    static IrisData irisData;
    bool running, debug;

    float step, currentStep, currentGrad;
    int stepNo, pointBudget;

    List<IRPath> allPaths;
    List<int> alivePaths;
    #endregion

    #region Constructor
    public Iris(IrisData _irisData, bool debugMode)
    {
        irisData = _irisData;
        debug = debugMode;
        running = true;

        stepNo = 0;
        currentStep = 0;
        currentGrad = 0;
        step = irisData.totalRadius / irisData.radialSteps;

        allPaths = new List<IRPath>();

        // Debugging
        if (debug) Debug.Log("Iris created!");
    }
    #endregion

    #region Public Access
    public void Update()
    {
        // GUARD PATTERN to stop execution upon completion
        if (stepNo == irisData.radialSteps)
        {
            if (debug && running)
            {
                running = false;
                Debug.Log("iris compledted.");
            }
            return;
        }

        // CYCLE SETUP
        // Determine step of the current cycle
        currentStep = stepNo * step;
        // Determine gradient factor by sampling
        currentGrad = SampleGradient(currentStep);
        // Determine cycles point budget
        pointBudget = GetPointsBudget(currentGrad);
        // Retrieve IDs of alive paths
        alivePaths = GetAlivePaths();
        // Calculate how many points to spawn / kill
        int diffToPrev = pointBudget - alivePaths.Count;

        // PATH MANAGEMENT
        ManagePaths(diffToPrev);

        // EXTRUDE alive paths older than 1 cycle
        foreach (var id in alivePaths)
        {
            allPaths[id].Extrude(irisData, step, currentStep);
        }

        // DEBUGGING information is collected and displayed
        if (debug) Debug.Log(
            "CYCLE NO." + stepNo + "\n" +
            "currentStep = " + currentStep + "\n"
        );

        // COMPLETE CYCLE by calculationg next step number
        stepNo++;
    }

    public void DebugDraw()
    {
        foreach (var path in allPaths)
        {
            path.DebugDraw();
        }
    }
    #endregion

    #region Internal Methods
    private float SampleGradient(float radius)
    {
        float t = radius / irisData.totalRadius;
        Color sampleGradient = irisData.weightDistribution.Evaluate(t);
        Color.RGBToHSV(sampleGradient, out float h, out float s, out float v);
        return v;
    }

    private int GetPointsBudget(float weight)
    {
        int amount = Random.Range((int)(irisData.minStepResolution * weight),(int)((irisData.maxStepResolution + 1) * weight));
        // + 1 since when using Random.Range as int the upper range changes into an exclusive
        return amount;
    }

    private List<int> GetAlivePaths()
    {
        List<int> alive = new List<int>();

        int i = 0;
        foreach (var path in allPaths)
        {
            if (path.IsAlive()) alive.Add(i);
            i++;
        }
        return alive;
    }

    private void ManagePaths(int diffToPrev)
    {
        if (diffToPrev == 0)
        {
            return;
        }
        else if (diffToPrev < 0)
        {
            diffToPrev = -diffToPrev;
            List<int> prey = util.RandomIDs(diffToPrev, alivePaths);

            foreach (var id in prey)
            {
                allPaths[id].Kill();
                alivePaths.Remove(id);
            }
        }
        else
        {
            for (int i = 0; i < diffToPrev; i++)
            {
                allPaths.Add(new IRPath(currentStep));
            }
        }
    }
    #endregion

    #region Subclasses and Structs
    public class IRPath
    {
        #region Properties
        private bool alive;
        private List<IRPoint> points;
        #endregion

        #region Constructor
        public IRPath(float radius)
        {
            alive = true;
            points = new List<IRPoint>();
            AddPoint(radius);
        }
        #endregion

        #region Public Access
        public void AddPoint(float radius)
        {
            IRPoint point = CreateRandomPoint(radius);
            Debug.Log("point: " + point.position + " | " + point.step);
            points.Add(point);
        }

        public void Extrude(IrisData irisData, float step, float currentStep)
        {
            Vector3 position =  points[points.Count - 1].position;
            Vector3 displacement = (position - irisData.origin).normalized * step;
            displacement += util.RandomVector2D(irisData.displacementLimit);
            Vector3 result = position + displacement;
            result.z = GetDepthCoord(currentStep);
            IRPoint point = new IRPoint ( result, currentStep );
            points.Add(point);
        }

        public bool IsAlive()
        {
            if (alive == true)
            {
                return true;
            } else {
                return false;
            }
        }

        public void Kill()
        {
            alive = false;
        }

        public void DebugDraw()
        {
            for (int i = 0; i < points.Count - 1; i++)
            {
                Debug.DrawLine(points[i].position, points[i + 1].position, Color.white);
            }
        }
        #endregion

        #region Internal Methods
        private IRPoint CreateRandomPoint(float radius)
        {
            Vector3 pos = util.RandomCircleCoordinate(radius);
            pos.z = GetDepthCoord(radius);
            return new IRPoint (pos, radius);
        }
        
        private float GetDepthCoord (float radius)
        {
            float t = radius / irisData.totalRadius;
            Color sampleGradient = irisData.weightDistribution.Evaluate(t);
            Color.RGBToHSV(sampleGradient, out float h, out float s, out float v);
            return v * irisData.depthFactor;
        }
        #endregion
    }

    private struct IRPoint
    {
        internal float step;
        internal Vector3 position;

        internal IRPoint (Vector3 _pos, float _step)
        {
            this.position = _pos;
            this.step = _step;
        }
    }
    #endregion
}