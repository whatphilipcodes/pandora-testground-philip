using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "IrisData", menuName = "Pandora Customs/IrisData", order = 1)]

public class IrisData : ScriptableObject
{
    // Generation Settings
    public Vector3 origin;
    public float totalRadius, maxCylinderRadius;
    [Range(0f,0.005f)] public float displacementLimit;
    [Range(0f,0.1f)] public float depthFactor;
    public int radialSteps, minStepResolution, maxStepResolution;
    public Gradient weightDistribution;

    // SDF Settings
    [HideInInspector] public int maxResolution = 64, signPassCount = 1;
    [HideInInspector] public Vector3 center, sizeBox;
    [HideInInspector] public float threshold = 0.5f;

    // VFX Data
    public Texture3D SDF;
}
