using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GD.MinMaxSlider;

[CreateAssetMenuAttribute(fileName = "IrisData", menuName = "Pandora Customs/IrisData", order = 1)]

public class IrisData : ScriptableObject
{
    // Generation Settings
    public Vector3 origin;
    public float totalRadius;
    [Range(0f,0.005f)] public float displacementLimit;
    [Range(0f,0.2f)] public float depthFactor;
    [MinMaxSlider(0f,500f)] public Vector2Int minMaxStepResolution;
    public int radialSteps/*, minStepResolution, maxStepResolution*/;
    public AnimationCurve weightDistributionCurve;

    //public Gradient weightDistribution;

    // Mesh Settings
    public float maxCylinderRadius;

    // SDF Settings
    [HideInInspector] public int maxResolution = 64, signPassCount = 1;
    [HideInInspector] public Vector3 center, sizeBox;
    [HideInInspector] public float threshold = 0.5f;

    // VFX Data
    //public Texture3D SDF;
}
