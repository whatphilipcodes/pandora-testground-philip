using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path
{
    [SerializeField, HideInInspector]
    List<Vector3> points;

    public Path (Vector3 centre)
    {
        points = new List<Vector3>
        {
            centre + Vector3.left,
            centre + (Vector3.left + Vector3.up) * 0.5f,
            centre + (Vector3.right + Vector3.down) * 0.5f,
            centre + Vector3.right
        };
    }

    public Vector3 this[int i]
    {
        get
        {
            return points[i];
        }
    }

    public int numPoints
    {
        get
        {
            return points.Count;
        }
    }

    public int numSegments
    {
        get
        {
            return (points.Count - 4) / 3 + 1;
        }
    }

    public void AddSegment(Vector3 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2 - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) * 0.5f);
        points.Add(anchorPos);
    }

    public Vector3[] GetPointsInSegment( int i)
    {
        return new Vector3[]
        {
            points[i * 3],
            points[i * 3 + 1],
            points[i * 3 + 2],
            points[i * 3 + 3]
        };
    }

    public void MovePoint(int i, Vector3 pos)
    {
        points[i] = pos;
    }
}
