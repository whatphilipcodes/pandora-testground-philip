using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "IrisData", menuName = "Pandora Customs/IrisData", order = 1)]

public class IrisData : ScriptableObject
{
    // Generation Settings
    public Vector3 origin;
    public float totalRadius, maxPointRadius, displacementLimit;
    public int maxPointsPerRadius, radialSteps, numInitPoints;
    public Gradient stageDistribution;

    // SDF Settings
    [HideInInspector] public int maxResolution = 64, signPassCount = 1;
    [HideInInspector] public Vector3 center, sizeBox;
    [HideInInspector] public float threshold = 0.5f;

    // VFX Data
    public Texture3D SDF;
}
