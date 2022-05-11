using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenuAttribute(fileName = "IrisSettings", menuName = "Pandora Customs/IrisSettings", order = 1)]

public class irisSettings : ScriptableObject
{
    public Vector3 origin;
    public float radius, thickness;
    public int resolution;
    [Range (0, 1)] public float noiseAmount;
    public Mesh mesh;

    public int maxResolution = 64;
    public Vector3 center;
    public Vector3 sizeBox;
    public int signPassCount = 1;
    public float threshold = 0.5f;
}
